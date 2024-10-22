using Cbr.Domain.Entity;
using Cbr.Infrastructure.Database.Repository;

namespace Cbr.Infrastructure.Service;

public class CurrencyRatesService(CurrencyRatesRepository currencyRatesRepository)
{
    private readonly CurrencyRatesRepository _currencyRatesRepository = currencyRatesRepository;
    
    public async Task<CurrencyRates?> GetDayRatesByDate(DateOnly date, CancellationToken ct) => await _currencyRatesRepository.GetByDate(date, ct);
    
    public void SaveDayRatesFromRaw(string rawXml)
    {
        CurrencyRates rates = CbrXmlParser.FromRawString(rawXml);

        _currencyRatesRepository.Add(rates);

        _currencyRatesRepository.SaveChanges();
    }

    public void SaveDayRatesFromFile(string filepath)
    {
        CurrencyRates rates = CbrXmlParser.FromFile(filepath);

        _currencyRatesRepository.Add(rates);

        _currencyRatesRepository.SaveChanges();
    }
}
