using System.Xml;
using Cbr.Domain.Entity;

namespace Cbr.Infrastructure.Service;

internal static class CbrConverter
{
    public static List<CurrencyRate> ToCbrCurrencyRates(XmlElement el)
    {
        List<CurrencyRate> currencies = [];
        Currency rub = new("RUB", "Российских рублей");
        DateOnly date = DateOnly.ParseExact(el.Attributes["Date"]!.Value, "dd.MM.yyyy");

        foreach (XmlElement currencyXml in el)
        {
            if (currencyXml.Name is not "Valute")
            {
                throw new FormatException("Unexpected tag identifier");
            }
            List<KeyValuePair<string, string>> data = [];
            foreach (XmlNode node in currencyXml)
            {
                data.Add(new(node.Name, node.ChildNodes.Item(0)!.Value!));
            }

            var parsedCurrency = new Currency(
                data.Single(pair => pair.Key == "CharCode").Value,
                data.Single(pair => pair.Key == "Name").Value);

            currencies.Add(new CurrencyRate(parsedCurrency, rub, date, decimal.Parse(data.Single(pair => pair.Key == "VunitRate").Value)));
        }

        return currencies;
    }
}