using Cbr.Application.Abstractions;
using Cbr.Domain.Entity;
using Cbr.Infrastructure.Database.Repository;

namespace Cbr.Application.Service;

public class CurrencyRatesService(CurrencyRateRepository currencyRatesRepository, ICbrXmlParser cbrXmlParser)
{
    private readonly CurrencyRateRepository _currencyRatesRepository = currencyRatesRepository;
    private readonly ICbrXmlParser _cbrXmlParser = cbrXmlParser;


    public async Task<List<CurrencyRate>> ListDayRatesByDate(DateOnly date, CancellationToken ct) 
        => await _currencyRatesRepository.GetAllByDate(date, ct);

    public void ImportCbrCurrencyRates(string filepath)
    {
        List<CurrencyRate> rates = _cbrXmlParser.FromFile(filepath);

        _currencyRatesRepository.AddRange(rates);

        _currencyRatesRepository.SaveChanges();
    }
}
