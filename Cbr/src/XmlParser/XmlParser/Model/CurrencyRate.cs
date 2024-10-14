namespace XmlParser.Model;

public class CurrencyRate
{
    public string Id { get; }
    public ushort NumCode { get; }
    public string CharCode { get; }
    public uint Nominal { get; }
    public string Name { get; }
    public decimal Value { get; }
    public decimal VUnitRate { get; }

    public CurrencyRate(string id, 
        ushort numCode, 
        string charCode, 
        uint nominal, 
        string name, 
        decimal value,
        decimal vUnitRate)
    {
        Id = id;
        NumCode = numCode;
        CharCode = charCode;
        Nominal = nominal;
        Name = name;
        Value = value;
        VUnitRate = vUnitRate;
    }
}