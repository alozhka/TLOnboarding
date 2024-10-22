namespace Cbr.Domain.Entity;


public class Currency
{
    /// <summary>
    /// Код валюты (пр. RUB, USD и тд)
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Полное название (пр. Доллар США)
    /// </summary>
    public string Name { get; }

    public Currency(string code, string name)
    {
        Code = code;
        Name = name;
    }

    /// <summary>
    /// For EF
    /// </summary>
    public Currency()
    {
    }
}
