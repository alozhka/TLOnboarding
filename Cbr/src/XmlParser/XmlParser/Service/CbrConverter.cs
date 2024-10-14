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
            List<string> data = [];
            foreach (XmlElement value in currency)
            {
                data.Add(value.ChildNodes.Item(0)!.Value!);
            }

            currencies.Add(new CurrencyRate(
                data[1], 
                data[3],
                decimal.Parse(data[5])));
        }

        return new CurrencyRates(DateOnly.ParseExact(el.Attributes["Date"]!.Value, "dd.MM.yyyy"), currencies);
    }
}