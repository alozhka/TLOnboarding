using Cbr.Application.Abstractions;
using Cbr.Application.Dto;
using Cbr.Domain.Entity;

namespace Cbr.Application.Service;

public class CurrencyRatesService(
    ICurrencyRateRepository currencyRatesRepository, 
    ICbrXmlParser cbrXmlParser, 
    ICurrencyRepository currencyRepository)
{
    private readonly ICurrencyRateRepository _currencyRatesRepository = currencyRatesRepository;
    private readonly ICurrencyRepository _currencyRepository = currencyRepository;
    private readonly ICbrXmlParser _cbrXmlParser = cbrXmlParser;


    public async Task<CbrDayRatesDto?> ListDayRatesByDate(DateOnly date, CancellationToken ct)
        => await _currencyRatesRepository.ListCbrDayRatesToRub(date, ct);

    public void ImportCbrCurrencyRates(string filepath)
    {
        List<CurrencyRate> rates = _cbrXmlParser.FromFile(filepath);
        PersistRates(rates);
    }

    public void ImportCbrCurrencyRatesFromRaw(string rawXml)
    {
        List<CurrencyRate> rates = _cbrXmlParser.FromFile(rawXml);
        PersistRates(rates);
    }

    private void PersistRates(List<CurrencyRate> rates)
    {
        List<string> currencyCodes = rates.Select(cr => cr.SourceCurrency.Code).Append("RUB").ToList();
        List<string> persistedCodes = _currencyRepository.ListCurrencyCodesRange(currencyCodes, default).Result;

        List<CurrencyRate> ratesToAdd = rates
            .Where(cr => !persistedCodes.Contains(cr.SourceCurrencyCode) || !persistedCodes.Contains(cr.TargetCurrencyCode))
            .ToList();


        List<CurrencyRate> ratesToUpdate = rates
            .Where(cr => persistedCodes.Contains(cr.SourceCurrencyCode) && persistedCodes.Contains(cr.TargetCurrencyCode))
            .ToList();

        _currencyRatesRepository.AddRange(ratesToAdd);
        _currencyRatesRepository.UpdateRange(ratesToUpdate);

        _currencyRatesRepository.SaveChanges();
    }
}
