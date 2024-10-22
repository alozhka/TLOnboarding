namespace Cbr.Domain.Entity;


public class Currency
{
    /// <summary>
    /// Код валюты (пр. RUB, USD и тд)
    /// </summary>
    public string CharCode { get; }

    /// <summary>
    /// Полное название (пр. Доллар США)
    /// </summary>
    public string Name { get; }

    public Currency(string charCode, string name)
    {
        CharCode = charCode;
        Name = name;
    }

    /// <summary>
    /// For EF
    /// </summary>
    public Currency()
    {
    }
}
