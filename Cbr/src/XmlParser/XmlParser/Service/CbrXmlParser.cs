using System.Text;
using System.Xml;
using XmlParser.Model;
namespace XmlParser.Service;

public class CbrXmlParser
{
    public static CurrencyRates FromFile(string filepath)
    {
        SetupEncoding();

        return LoadAndParse(filepath);
    }

    public static CurrencyRates FromRawString(string rawXml)
    {
        SetupEncoding();


        return LoadAndParse(rawXml);
    }

    private static CurrencyRates LoadAndParse(string loadArg)
    {
        XmlDocument doc = new();

        try
        {
            doc.LoadXml(loadArg);
            XmlElement? rootElement = doc.DocumentElement;
            if (rootElement is null)
            {
                throw new InvalidDataException("No data inside xml-document");
            }

            return rootElement.ToCurrencyRate();
        }
        catch (IndexOutOfRangeException)
        {
            throw new FormatException("Invalid document format");
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new FormatException("Invalid document format");
        }
        catch (XmlException)
        {
            throw new FormatException("Invalid document format");
        }
    }

    private static void SetupEncoding()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }
}