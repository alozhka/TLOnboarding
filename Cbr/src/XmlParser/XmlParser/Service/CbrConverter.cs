using System.Xml;
using XmlParser.Model;

namespace XmlParser.Service;

public static class CbrConverter
{
    public static CurrencyRates ToCurrencyRate(this XmlElement el)
    {
        List<CurrencyRate> currencies = [];
        foreach (XmlElement currency in el)
        {
            List<string> data = [];
            foreach (XmlElement value in currency)
            {
                data.Add(value.ChildNodes.Item(0).Value);
            }

            currencies.Add(new CurrencyRate(currency.Attributes.Item(0).Value, 
                ushort.Parse(data[0]), 
                data[1], 
                uint.Parse(data[2]), 
                data[3],
                decimal.Parse(data[4]), 
                decimal.Parse(data[5])));
        }

        return new CurrencyRates(el.Attributes["name"].Value, DateOnly.ParseExact(el.Attributes["Date"].Value, "dd.MM.yyyy"), currencies);
    }
}