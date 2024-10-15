namespace Cbr.Domain.Entity;

/// <summary>
/// Показывает стоимость валют к российскому рублю за определённую дату
/// </summary>
public class CurrencyRates(DateOnly date, List<CurrencyRate> currencies)
{
    /// <summary>
    /// Дата, для которой действителен курс валют
    /// </summary>
    public DateOnly Date { get; } = date;

    /// <summary>
    /// Список валют для конкретной даты
    /// </summary>
    public List<CurrencyRate> Currencies { get; } = currencies;

    public void AppendCurrency(CurrencyRate currency)
    {
        Currencies.Add(currency);
    }
}