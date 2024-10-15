using Cbr.Domain.Entity;
using Cbr.Infrastructure.Service;

namespace Cbr.UnitTests;

public class XmlParserTests
{
    [Fact]
    public void Can_use_complex_types()
    {
        CurrencyRates rate = CbrXmlParser.FromRawString(
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

        CurrencyRates expected = new CurrencyRates(
            new DateOnly(2024, 10, 8),
            new List<CurrencyRate>([
                new CurrencyRate(
                    "AUD",
                    "Австралийский доллар",
                    65.7852m),
                new CurrencyRate(
                    "AZN",
                    "Азербайджанский манат",
                    56.5088m)
            ]));

        Assert.Equivalent(expected, rate);
    }

    [Fact]
    public void Supports_parsing_from_xml_file()
    {
        CurrencyRates rate = CbrXmlParser.FromFile("../../../data/XML_daily.xml");

        CurrencyRates expected = new(
            new DateOnly(2024, 10, 8),
            new List<CurrencyRate>([
                new CurrencyRate(
                    "AUD",
                    "Австралийский доллар",
                    65.7852m),
                new CurrencyRate(
                    "AZN",
                    "Азербайджанский манат",
                    56.5088m),
                new CurrencyRate(
                    "JPY",
                    "Японских иен",
                    0.647076m)
            ]));

        Assert.Equivalent(expected, rate);
    }

    [Fact]
    public void Parse_only_cbr_daily_response_type()
    {
        Assert.Throws<FormatException>(() => CbrXmlParser.FromRawString(
            """
            <?xml version="1.0" encoding="windows-1251"?>
            <ValCurs Date="08.10.2024" name="Foreign Currency Market">
                <Valute ID="R01010">
                    <CharCode>AUD</CharCode>
                    <Name>Австралийский доллар</Name>
                    <VunitRate>string</VunitRate>
                </Valute>
            </ValCurs> 
            """));

        Assert.Throws<FormatException>(() => CbrXmlParser.FromRawString(
            """
            <?xml version="1.0" encoding="windows-1251"?>
            <UnexpectedTag Date="08.10.2024" name="Foreign Currency Market">
                <Valute ID="R01010">
                    <CharCode>AUD</CharCode>
                    <Name>Австралийский доллар</Name>
                    <VunitRate>string</VunitRate>
                </Valute>
            </UnexpectedTag>
            """));

        Assert.Throws<FormatException>(() => CbrXmlParser.FromRawString(
        """
            <?xml version="1.0" encoding="windows-1251"?>
            <ValCurs Date="08.10.2024" name="Foreign Currency Market">
                <UnexpectedTag ID="R01010">
                    <CharCode>AUD</CharCode>
                    <Name>Австралийский доллар</Name>
                    <VunitRate>string</VunitRate>
                </UnexpectedTag>
            </ValCurs>
            """));
    }

    [Fact]
    public void Cannot_parse_incorrect_xml()
    {
        Assert.Throws<FormatException>(() => CbrXmlParser.FromFile("../../../data/failure/XML_wrong.asp"));

        Assert.Throws<FormatException>(() => CbrXmlParser.FromRawString(
            """
            <?xml version="1.0" encoding="windows-1251"?>
            <ValCurs Date="08.10.2024" name="Foreign Currency Market">
                <Valute ID="R01010">
                    <NumCode>036</NumCode>
                <Valute ID="R01020A"><NumCode>944</NumCode><CharCode>AZN</CharCode><Nominal>1</Nominal>
                    <Name>Азербайджанский манат</Name><Value>56,5088</Value><VunitRate>56,5088</VunitRate>
                </Valute>
            </ValCurs> 
            """));

        Assert.Throws<FormatException>(() => CbrXmlParser.FromFile("../../../data/failure/XML_empty.asp"));

        Assert.Throws<FileNotFoundException>(() => CbrXmlParser.FromFile("does not exist"));
    }
}