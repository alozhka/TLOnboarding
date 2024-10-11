using XmlParser.Model;
using Xunit;

namespace XmlParser.Tests;

public class XmlParserTests
{
    [Fact]
    public void Can_use_complex_types()
    {
        XmlElement el = Service.XmlParser.FromRawString(
            """
            <?xml version="1.0" encoding="utf-8"?>
            <Element attr1="12=3" attr2="456">
                <name>Alex</name>
            </Element>
            """);
    }

    [Fact]
    public void Supports_parsing_from_xml_file()
    {
        XmlElement el = Service.XmlParser.FromFile("../../../data/XML_daily.asp");
        
    }

    [Fact]
    public void Cannot_parse_incorrect_xml()
    {
    }
}