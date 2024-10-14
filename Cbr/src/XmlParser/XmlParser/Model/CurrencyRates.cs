namespace XmlParser.Model;

public class CurrencyRates
{
    public CurrencyRates(string name, DateOnly date, List<CurrencyRate> currencies)
    {
        Name = name;
        Date = date;
        Currencies = currencies;
    }

    public string Name { get; }
    public DateOnly Date { get; }
    public List<CurrencyRate> Currencies { get; }

    public void AppendCurrency(CurrencyRate currency)
    {
        Currencies.Add(currency);
    }
}