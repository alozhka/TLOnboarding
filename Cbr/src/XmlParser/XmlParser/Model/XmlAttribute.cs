namespace XmlParser.Model;

public class XmlAttribute(string name, string value)
{
    public string Name { get; set; } = name;
    public string Value { get; set; } = value;
}