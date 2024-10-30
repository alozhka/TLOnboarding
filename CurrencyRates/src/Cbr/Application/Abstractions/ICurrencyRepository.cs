using Cbr.Domain.Entity;

namespace Cbr.Application.Abstractions;

public interface ICurrencyRepository
{
    void AddOrUpdateRange(List<Currency> currencies);
}
