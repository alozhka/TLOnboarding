using System.Xml;
using Cbr.Domain.Entity;

namespace Cbr.Infrastructure.Service;

internal static class CbrConverter
{
    public static CurrencyRates ToCurrencyRate(XmlElement el)
    {
        List<CurrencyRate> currencies = [];

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

            Currency currency = new(
                currencyCode: data.Single(pair => pair.Key == "CharCode").Value, 
                currencyName: data.Single(pair => pair.Key == "Name").Value);

            currencies.Add(new CurrencyRate(currency, exchangeRate: decimal.Parse(data.Single(pair => pair.Key == "VunitRate").Value)));
        }

        return new CurrencyRates(DateOnly.ParseExact(el.Attributes["Date"]!.Value, "dd.MM.yyyy"), currencies);
    }
}