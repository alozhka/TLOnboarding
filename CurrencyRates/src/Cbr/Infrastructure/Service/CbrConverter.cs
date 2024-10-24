using System.Xml;
using Cbr.Application.Dto;
using Cbr.Domain.Entity;

namespace Cbr.Infrastructure.Service;

internal static class CbrConverter
{
    public static CbrDayRatesDto ToCbrDayRatesDto(XmlElement el)
    {
        List<CbrRateDto> currencies = [];
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

            currencies.Add(new CbrRateDto(
                data.Single(pair => pair.Key == "CharCode").Value, 
                data.Single(pair => pair.Key == "Name").Value, 
                decimal.Parse(data.Single(pair => pair.Key == "VunitRate").Value)));
        }

        return new CbrDayRatesDto(date.ToString("dd.MM.yyyy"), currencies);
    }
}