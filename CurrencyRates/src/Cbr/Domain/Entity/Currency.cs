namespace Cbr.Domain.Entity;

/// <summary>
/// Отражает валюту
/// </summary>
public class Currency
{
    /// <summary>
    /// Код валюты (пр. RUB, USD и тд)
    /// </summary>
    public string CurrencyCode { get; }

    /// <summary>
    /// Полное название (пр. Доллар США)
    /// </summary>
    public string CurrencyName { get; }

    public Currency(string currencyCode, string currencyName)
    {
        CurrencyCode = currencyCode;
        CurrencyName = currencyName;
    }
    
    /// <summary>
    /// For EF
    /// </summary>
    public Currency()
    {
    }
}
