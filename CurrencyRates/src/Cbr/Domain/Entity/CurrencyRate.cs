namespace Cbr.Domain.Entity;

/// <summary>
/// Показывает стоимость валюты к российскому рублю
/// </summary>
public class CurrencyRate
{
    public Currency Currency { get; }

    /// <summary>
    /// Стоимость 1 единицы валюты в рублёвом эквиваленте (пр. USD/RUB = 96.89)
    /// </summary>
    public decimal ExchangeRate { get; }

    public CurrencyRate(
        Currency currency,
        decimal exchangeRate)
    {
        Currency = currency;
        ExchangeRate = exchangeRate;
    }

    /// <summary>
    /// For EF
    /// </summary>
    public CurrencyRate()
    {
    }
}