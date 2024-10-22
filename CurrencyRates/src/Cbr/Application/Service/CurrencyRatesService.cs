using Cbr.Application.Abstractions;
using Cbr.Application.Dto;
using Cbr.Domain.Entity;

namespace Cbr.Application.Service;

public class CurrencyRatesService(ICurrencyRateRepository currencyRatesRepository, ICbrXmlParser cbrXmlParser)
{
    private readonly ICurrencyRateRepository _currencyRatesRepository = currencyRatesRepository;
    private readonly ICbrXmlParser _cbrXmlParser = cbrXmlParser;


    public async Task<CbrDayRatesDto?> ListDayRatesByDate(DateOnly date, CancellationToken ct) 
        => await _currencyRatesRepository.ListCbrDayRatesToRub(date, ct);

    public void ImportCbrCurrencyRates(string filepath)
    {
        List<CurrencyRate> rates = _cbrXmlParser.FromFile(filepath);

        _currencyRatesRepository.AddRange(rates);

        _currencyRatesRepository.SaveChanges();
    }
}
