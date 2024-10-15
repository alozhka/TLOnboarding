using Cbr.Domain.Entity;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRateDate
{
    public string CurrencyRateCharCode { get; }
    public string Date { get; }
    public CurrencyRate CurrencyRate { get; } = null!;
    public decimal VunitRate { get; }
}
