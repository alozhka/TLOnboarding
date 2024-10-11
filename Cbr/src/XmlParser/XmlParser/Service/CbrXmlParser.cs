using System.Text;
using System.Xml;
using XmlParser.Model;
using XmlElement = System.Xml.XmlElement;

namespace XmlParser.Service;

public class CbrXmlParser
{
    public static CurrencyRate FromFile(string filepath)
    {
        SetupEncoding();

        XmlDocument doc = new();
        doc.Load(filepath);
        XmlElement? el = doc.DocumentElement;
        if (el is null)
        {
            throw new InvalidDataException("No data inside xml-document");
        }

        try
        {
            return el.ToCurrencyRate();
        }
        catch (Exception)
        {
            throw new FormatException("Invalid document format");
        }
    }

    public static CurrencyRate FromRawString(string rawXml)
    {
        SetupEncoding();

        XmlDocument doc = new();
        doc.LoadXml(rawXml);

        XmlElement? el = doc.DocumentElement;
        if (el is null)
        {
            throw new InvalidDataException("No data inside xml-document");
        }
        
        try
        {
            return el.ToCurrencyRate();
        }
        catch (Exception)
        {
            throw new FormatException("Invalid document format");
        }
    }
    
    private static void SetupEncoding()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }
}