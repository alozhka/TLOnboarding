namespace Cbr.Domain.Entity;

/// <summary>
/// Показывает стоимость валюты к российскому рублю
/// </summary>
public class CurrencyRate
{
    /// <summary>
    /// Код валюты (пр. RUB, USD и тд)
    /// </summary>
    public string CharCode { get; }

    /// <summary>
    /// Полное название (пр. Доллар США)
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Стоимость 1 единицы валюты в рублёвом эквиваленте (пр. USD/RUB = 96.89)
    /// </summary>
    public decimal VUnitRate { get; }

    public CurrencyRate(
        string charCode,
        string name,
        decimal vUnitRate)
    {
        CharCode = charCode;
        Name = name;
        VUnitRate = vUnitRate;
    }

    /// <summary>
    /// For EF
    /// </summary>
    public CurrencyRate()
    {
    }
}