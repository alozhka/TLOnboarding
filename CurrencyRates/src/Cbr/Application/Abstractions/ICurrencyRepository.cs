using Cbr.Domain.Entity;

namespace Cbr.Application.Abstractions;

public interface ICurrencyRepository
{
    void AddRange(List<Currency> currencies);
    void UpdateRange(List<Currency> currencies);
    void AddOrUpdateRange(List<Currency> currencies);
}
