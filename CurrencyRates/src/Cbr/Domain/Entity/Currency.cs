namespace Cbr.Domain.Entity;


public class Currency : IEquatable<Currency>
{
    /// <summary>
    /// Код валюты (пр. RUB, USD и тд)
    /// </summary>
    public string Code { get; } = null!;

    /// <summary>
    /// Полное название (пр. Доллар США)
    /// </summary>
    public string Name { get; set; } = null!;

    public Currency(string code, string name)
    {
        Code = code;
        Name = name;
    }

    /// <summary>
    /// For EF
    /// </summary>
    protected Currency()
    {
    }

    public bool Equals(Currency? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return Code == other.Code && Name == other.Name;
    }
}
