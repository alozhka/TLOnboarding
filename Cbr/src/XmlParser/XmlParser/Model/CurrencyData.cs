namespace XmlParser.Model;

public class CurrencyData
{
    public string Id { get; private set; }
    public ushort NumCode { get; private set; }
    public string CharCode { get; private set; }
    public uint Nominal { get; private set; }
    public string Name { get; private set; }
    public decimal Value { get; private set; }
    public decimal VUnitRate { get; private set; }

    public CurrencyData(string id, 
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