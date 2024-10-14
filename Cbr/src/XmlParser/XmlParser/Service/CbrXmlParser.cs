using System.Text;
using System.Xml;
using XmlParser.Model;
namespace XmlParser.Service;

public class CbrXmlParser
{
    /// <summary>
    /// Парсит xml-файл в сущность <see cref="CurrencyRates"/>
    /// </summary>
    /// <param name="filepath">Путь до xml-файла</param>
    public static CurrencyRates FromFile(string filepath)
    {
        SetupEncoding();

        string rawXml = File.ReadAllText(filepath, Encoding.GetEncoding("windows-1251"));
        return LoadAndParse(rawXml);
    }

    /// <summary>
    /// Парсит строку в сущность <see cref="CurrencyRates"/>
    /// </summary>
    /// <param name="rawXml">Xml-cтрока</param>
    public static CurrencyRates FromRawString(string rawXml)
    {
        SetupEncoding();

        return LoadAndParse(rawXml);
    }

    /// <summary>
    /// Парсит данные в xml элементы и переводит в предметную область
    /// </summary>
    /// <param name="loadArg">Аргумент для загрузки (путь до файла или xml-строка)</param>
    /// <returns></returns>
    /// <exception cref="FormatException">Неправильная разметка</exception>
    private static CurrencyRates LoadAndParse(string loadArg)
    {
        XmlDocument doc = new();

        try
        {
            doc.LoadXml(loadArg);
            XmlElement? rootElement = doc.DocumentElement
                ?? throw new FormatException("No data inside xml-document");
            return rootElement.ToCurrencyRate();
        }
        catch (FormatException)
        {
            throw;
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