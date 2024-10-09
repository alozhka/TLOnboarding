using XmlParser.Model;

namespace XmlParser.Service;

public static class XmlParser
{
    public static XmlElement FromFile(string filepath)
    {
        Stream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        return Parse(stream);
    }
    
    public static XmlElement FromRawString(string rawXml)
    {
        return Parse(rawXml.ToStream());
    }

    private static XmlElement Parse(Stream stream)
    {
    }

    private static Stream ToStream(this string str)
    {
        MemoryStream stream = new();
        StreamWriter writer = new(stream);
        writer.Write(str);
        stream.Flush();
        stream.Position = 0;
        return stream;
    }
}