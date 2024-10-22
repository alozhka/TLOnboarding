using System.Text;
using System.Xml;
using Cbr.Application.Abstractions;
using Cbr.Domain.Entity;

namespace Cbr.Infrastructure.Service;

public class CbrXmlParser : ICbrXmlParser
{
    /// <summary>
    /// Парсит xml-файл в сущность <see cref="CurrencyRates"/>
    /// </summary>
    /// <param name="filepath">Путь до xml-файла</param>
    public List<CurrencyRate> FromFile(string filepath)
    {
        SetupEncoding();

        string rawXml = File.ReadAllText(filepath, Encoding.GetEncoding("windows-1251"));
        return LoadAndParse(rawXml);
    }

    /// <summary>
    /// Парсит строку в сущность <see cref="CurrencyRates"/>
    /// </summary>
    /// <param name="rawXml">Xml-cтрока</param>
    public List<CurrencyRate> FromRawString(string rawXml)
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
    private static List<CurrencyRate> LoadAndParse(string loadArg)
    {
        XmlDocument doc = new();

        try
        {
            doc.LoadXml(loadArg);
            XmlElement? rootElement = doc.DocumentElement;

            if (rootElement is null || rootElement.Name is not "ValCurs")
            {
                throw new FormatException("No data inside xml-document");
            }

            return CbrConverter.ToCurrencyRates(rootElement);
        }
        catch (Exception e)
        {
            if (e is InvalidOperationException // not found in Single LINQ method
            || e is XmlException) // wrong xml markdown
            {
                throw new FormatException("Invalid currency rates document format: " + e.Message, e);
            }

            throw;
        }
    }

    private static void SetupEncoding()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }
}