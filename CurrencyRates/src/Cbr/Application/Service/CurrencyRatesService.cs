using Cbr.Application.Abstractions;
using Cbr.Domain.Entity;
using Cbr.Infrastructure.Database.Repository;

namespace Cbr.Application.Service;

public class CurrencyRatesService(CurrencyRatesRepository currencyRatesRepository, ICbrXmlParser cbrXmlParser)
{
    private readonly CurrencyRatesRepository _currencyRatesRepository = currencyRatesRepository;
    private readonly ICbrXmlParser _cbrXmlParser = cbrXmlParser;


    public async Task<CurrencyRates?> ListDayRatesByDate(DateOnly date, CancellationToken ct) => await _currencyRatesRepository.GetByDate(date, ct);

    public void ImportCbrCurrencyRates(string filepath)
    {
        CurrencyRates rates = _cbrXmlParser.FromFile(filepath);

        _currencyRatesRepository.Add(rates);

        _currencyRatesRepository.SaveChanges();
    }
}
