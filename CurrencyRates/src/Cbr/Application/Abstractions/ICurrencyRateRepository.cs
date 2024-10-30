using Cbr.Application.Dto;
using Cbr.Domain.Entity;

namespace Cbr.Application.Abstractions;

public interface ICurrencyRateRepository
{
    void AddOrUpdateRange(List<CurrencyRate> currencyRates);
    void SaveChanges();

    Task<CbrDayRatesDto?> ListCbrDayRatesToRub(DateOnly date, CancellationToken ct);
}
