using System.Text;
using System.Xml;
using XmlParser.Model;
namespace XmlParser.Service;

public class CbrXmlParser
{
    public static CurrencyRates FromFile(string filepath)
    {
        SetupEncoding();

        XmlDocument doc = new();
        doc.Load(filepath);

        return ConvertToRate(doc.DocumentElement);
    }

    public static CurrencyRates FromRawString(string rawXml)
    {
        SetupEncoding();

        XmlDocument doc = new();
        doc.LoadXml(rawXml);

        return ConvertToRate(doc.DocumentElement);
    }

    private static CurrencyRates ConvertToRate(XmlElement? rootElement)
    {
        if (rootElement is null)
        {
            throw new InvalidDataException("No data inside xml-document");
        }

        try
        {
            return rootElement.ToCurrencyRate();
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