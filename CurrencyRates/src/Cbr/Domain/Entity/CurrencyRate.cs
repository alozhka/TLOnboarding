namespace Cbr.Domain.Entity;

/// <summary>
/// Показывает стоимость валюты к российскому рублю
/// </summary>
public class CurrencyRate
{
    /// <summary>
    /// Из USD/RUB этим полем будет USD
    /// </summary>
    public string SourceCurrencyCode { get; }

    /// <summary>
    /// Из USD/RUB этим полем будет RUB
    /// </summary>
    public string TargetCurrencyCode { get; }


    /// <summary>
    /// Курс за определённую дату
    /// </summary>
    public DateOnly Date { get; }

    /// <summary>
    /// Стоимость 1 единицы валюты в рублёвом эквиваленте (пр. USD/RUB = 96.89)
    /// </summary>
    public decimal ExchangeRate { get; }

    public CurrencyRate(
        string sourceCurrencyCode,
        string targetCurrencyCode,
        DateOnly date,
        decimal exchangeRate)
    {
        SourceCurrencyCode = sourceCurrencyCode;
        TargetCurrencyCode = targetCurrencyCode;
        Date = date;
        ExchangeRate = exchangeRate;
    }

    /// <summary>
    /// For EF
    /// </summary>
    public CurrencyRate()
    {
    }
}