namespace XmlParser.Model;


public class CurrencyRate
{
    public string CharCode { get; }
    public string Name { get; }
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
}