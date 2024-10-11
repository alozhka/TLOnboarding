using XmlParser.Model;
using XmlParser.Service;
using Xunit;

namespace XmlParser.Tests;

public class XmlParserTests
{
    [Fact]
    public void Can_use_complex_types()
    {
        CurrencyRate el = CbrXmlParser.FromRawString(
            """
            <?xml version="1.0" encoding="windows-1251"?>
            <ValCurs Date="08.10.2024" name="Foreign Currency Market">
                <Valute ID="R01010"><NumCode>036</NumCode><CharCode>AUD</CharCode><Nominal>1</Nominal>
                    <Name>Австралийский доллар</Name><Value>65,7852</Value><VunitRate>65,7852</VunitRate>
                </Valute>
                <Valute ID="R01020A"><NumCode>944</NumCode><CharCode>AZN</CharCode><Nominal>1</Nominal>
                    <Name>Азербайджанский манат</Name><Value>56,5088</Value><VunitRate>56,5088</VunitRate>
                </Valute>
            </ValCurs>            
            """);

        CurrencyRate expected = new CurrencyRate(
            "Foreign Currency Market",
            new DateOnly(2024, 10, 8),
            new List<CurrencyData>([
                new CurrencyData(
                    "R01010",
                    036,
                    "AUD",
                    1,
                    "Австралийский доллар",
                    65.7852m,
                    65.7852m),
                new CurrencyData(
                    "R01020A",
                    944,
                    "AZN",
                    1,
                    "Азербайджанский манат",
                    56.5088m,
                    56.5088m)
            ]));

        Assert.Equal(expected, el);
    }

    [Fact]
    public void Supports_parsing_from_xml_file()
    {
        CurrencyRate rate = CbrXmlParser.FromFile("../../../data/XML_daily.asp");
    }

    [Fact]
    public void Cannot_parse_incorrect_xml()
    {
    }
}