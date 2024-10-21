namespace Cbr.Domain.Entity;

/// <summary>
/// Показывает стоимость валюты к российскому рублю
/// </summary>
public class CurrencyRate
{
    /// <summary>
    /// Код валюты (пр. RUB, USD и тд)
    /// </summary>
    public string CurrencyCode { get; }

    /// <summary>
    /// Полное название (пр. Доллар США)
    /// </summary>
    public string CurrencyName { get; }

    /// <summary>
    /// Стоимость 1 единицы валюты в рублёвом эквиваленте (пр. USD/RUB = 96.89)
    /// </summary>
    public decimal ExchangeRate { get; }

    public CurrencyRate(
        string currencyCode,
        string currencyName,
        decimal exchangeRate)
    {
        CurrencyCode = currencyCode;
        CurrencyName = currencyName;
        ExchangeRate = exchangeRate;
    }

    /// <summary>
    /// For EF
    /// </summary>
    public CurrencyRate()
    {
    }
}