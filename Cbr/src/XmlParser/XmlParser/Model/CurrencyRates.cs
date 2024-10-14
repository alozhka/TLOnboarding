namespace XmlParser.Model;

public class CurrencyRates
{
    public CurrencyRates(DateOnly date, List<CurrencyRate> currencies)
    {
        Date = date;
        Currencies = currencies;
    }

    public DateOnly Date { get; }
    public List<CurrencyRate> Currencies { get; }

    public void AppendCurrency(CurrencyRate currency)
    {
        Currencies.Add(currency);
    }
}