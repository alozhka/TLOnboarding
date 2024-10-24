using Cbr.Domain.Entity;

namespace Cbr.Application.Abstractions;

public interface ICurrencyRepository
{
    Task<List<string>> ListCurrencyCodesRange(List<string> currencyCodes, CancellationToken ct);
}
