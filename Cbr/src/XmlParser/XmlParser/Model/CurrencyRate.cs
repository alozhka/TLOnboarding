namespace XmlParser.Model;

/// <summary>
/// Показывает стоимость валюты к российскому рублю
/// </summary>
public class CurrencyRate(
    string charCode,
    string name,
    decimal vUnitRate)
{
    /// <summary>
    /// Код валюты (пр. RUB, USD и тд)
    /// </summary>
    public string CharCode { get; } = charCode;

    /// <summary>
    /// Полное название (пр. Доллар США)
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Стоимость 1 единицы валюты в рублёвом эквиваленте (пр. USD/RUB = 96.89)
    /// </summary>
    public decimal VUnitRate { get; } = vUnitRate;
}