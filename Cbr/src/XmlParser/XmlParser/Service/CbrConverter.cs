using System.Xml;
using XmlParser.Model;

namespace XmlParser.Service;

internal static class CbrConverter
{
    public static CurrencyRates ToCurrencyRate(this XmlElement el)
    {
        List<CurrencyRate> currencies = [];
        foreach (XmlElement currency in el)
        {
            List<KeyValuePair<string,string>> data = [];
            foreach (XmlNode node in currency)
            {
                data.Add(new(node.Name, node.ChildNodes.Item(0)!.Value!));
            }

            currencies.Add(new CurrencyRate(
                data.Find(pair => pair.Key == "CharCode").Value, 
                data.Find(pair => pair.Key == "Name").Value,
                decimal.Parse(data.Find(pair => pair.Key == "VunitRate").Value)));
        }

        return new CurrencyRates(DateOnly.ParseExact(el.Attributes["Date"]!.Value, "dd.MM.yyyy"), currencies);
    }
}