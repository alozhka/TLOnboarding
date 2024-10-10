using Xunit;

namespace XmlParser.Tests;

public class XmlParserTests
{
    [Fact]
    public void Can_use_complex_types()
    {
        Service.XmlParser parser = new();
        parser.FromRawString(
            """
            <?xml version="1.0" encoding="utf-8"?>
            <Element attr1="123" attr2="456">
                <name>Alex</name>
            </Element>
            """);
    }

    [Fact]
    public void Supports_parsing_from_raw_data_and_from_xml_file()
    {
    }

    [Fact]
    public void Cannot_parse_incorrect_xml()
    {
    }
}