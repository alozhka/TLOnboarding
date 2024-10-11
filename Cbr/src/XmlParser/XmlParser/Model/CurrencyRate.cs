namespace XmlParser.Model;

public class CurrencyRate
{
    public CurrencyRate(string name, DateOnly date, List<CurrencyData> currencies)
    {
        Name = name;
        Date = date;
        Currencies = currencies;
    }

    public string Name { get; private set; }
    public DateOnly Date { get; private set; }
    public List<CurrencyData> Currencies { get; private set; }

    public void AppendCurrency(CurrencyData currency)
    {
        Currencies.Add(currency);
    }
}