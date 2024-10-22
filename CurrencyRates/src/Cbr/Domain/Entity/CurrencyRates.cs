namespace Cbr.Domain.Entity;

/// <summary>
/// Показывает стоимость валют к российскому рублю за определённую дату
/// </summary>
public class CurrencyRates
{
    /// <summary>
    /// Дата, для которой действителен курс валют
    /// </summary>
    public DateOnly Date { get; }

    /// <summary>
    /// Список валют для конкретной даты
    /// </summary>
    public List<CurrencyRate> Rates { get; }

    public CurrencyRates(DateOnly date, List<CurrencyRate> rates)
    {
        Date = date;
        Rates = rates;
    }
    
    /// <summary>
    /// For EF
    /// </summary>
    public CurrencyRates()
    {
    }

    public void AddRate(CurrencyRate rate)
    {
        Rates.Add(rate);
    }
}