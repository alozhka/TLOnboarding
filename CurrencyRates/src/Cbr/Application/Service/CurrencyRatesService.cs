using Cbr.Application.Abstractions;
using Cbr.Application.Dto;
using Cbr.Domain.Entity;
using Cbr.Infrastructure.Service;

namespace Cbr.Application.Service;

public class CurrencyRatesService(
    ICurrencyRateRepository currencyRatesRepository,
    ICurrencyRepository currencyRepository,
    ICbrXmlParser cbrXmlParser,
    ICbrApiService cbrApiService)
{
    private readonly ICurrencyRateRepository _currencyRatesRepository = currencyRatesRepository;
    private readonly ICurrencyRepository _currencyRepository = currencyRepository;
    private readonly ICbrXmlParser _cbrXmlParser = cbrXmlParser;
    private readonly ICbrApiService _cbrApiService = cbrApiService;


    public async Task<CbrDayRatesDto?> ListDayRatesByDate(DateOnly date, CancellationToken ct)
        => await _currencyRatesRepository.ListCbrDayRatesToRub(date, ct);

    public void ImportCbrCurrencyRates(string filepath)
    {
        CbrDayRatesDto rates = _cbrXmlParser.FromFile(filepath);
        PersistRates(rates);
    }

    public void ImportCbrCurrencyRatesFromRaw(string rawXml)
    {
        CbrDayRatesDto rates = _cbrXmlParser.FromRawString(rawXml);
        PersistRates(rates);
    }

    public async Task ImportFromCbrApiAsync(DateOnly date, CancellationToken ct)
    {
        string rawXml = await _cbrApiService.GetCbrDayRatesRaw(date, ct);
        CbrDayRatesDto rates = _cbrXmlParser.FromRawString(rawXml);
        PersistRates(rates);
    }

    private void PersistRates(CbrDayRatesDto rates)
    {
        DateOnly ratesDate = DateOnly.ParseExact(rates.Date, "dd.MM.yyyy");
        
        List<CurrencyRate> currencyRates = rates.Rates.Select(cr => new CurrencyRate(cr.CurrencyCode, "RUB", ratesDate, cr.ExchangeRate)).ToList();
        List<Currency> currencies = rates.Rates.Select(cr => new Currency(cr.CurrencyCode, cr.CurrencyName)).ToList();

        _currencyRepository.AddOrUpdateRange(currencies);
        _currencyRatesRepository.AddRange(currencyRates);

        _currencyRatesRepository.SaveChanges();
    }
}
