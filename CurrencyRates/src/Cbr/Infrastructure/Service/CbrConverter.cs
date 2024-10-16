using System.Xml;
using Cbr.Domain.Entity;

namespace Cbr.Infrastructure.Service;

internal static class CbrConverter
{
    public static CurrencyRates ToCurrencyRate(XmlElement el)
    {
        List<CurrencyRate> currencies = [];

        foreach (XmlElement currency in el)
        {
            if (currency.Name is not "Valute")
            {
                throw new FormatException("Unexpected tag identifier");
            }
            List<KeyValuePair<string, string>> data = [];
            foreach (XmlNode node in currency)
            {
                data.Add(new(node.Name, node.ChildNodes.Item(0)!.Value!));
            }

            currencies.Add(new CurrencyRate(
                data.Single(pair => pair.Key == "CharCode").Value,
                data.Single(pair => pair.Key == "Name").Value,
                decimal.Parse(data.Single(pair => pair.Key == "VunitRate").Value)));
        }

        return new CurrencyRates(DateOnly.ParseExact(el.Attributes["Date"]!.Value, "dd.MM.yyyy"), currencies);
    }
}