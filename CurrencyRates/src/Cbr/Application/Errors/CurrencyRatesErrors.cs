using Cbr.Application.Abstractions;

namespace Cbr.Application.Errors;

public static class CurrencyRatesErrors
{
    public static readonly Error _notFound = new("CurrencyRates.NotFound", "Котировки не найдены");
}
