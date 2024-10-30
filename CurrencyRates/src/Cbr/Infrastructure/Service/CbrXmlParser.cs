using System.Text;
using System.Xml;
using Cbr.Application.Abstractions;
using Cbr.Application.Dto;

namespace Cbr.Infrastructure.Service;


/// <summary>
/// Парсит xml из файла или строки в сущность <see cref="CbrDayRatesDto"/>
/// !!!Обязательно!!! Добавьте поддержку кодировки windows-1251
/// </summary>
public class CbrXmlParser : ICbrXmlParser
{
    /// <summary>
    /// Парсит xml-файл в сущность <see cref="CurrencyRates"/>
    /// </summary>
    /// <param name="filepath">Путь до xml-файла</param>
    public CbrDayRatesDto FromFile(string filepath)
    {
        string rawXml = File.ReadAllText(filepath, Encoding.GetEncoding("windows-1251"));
        return LoadAndParse(rawXml);
    }

    /// <summary>
    /// Парсит строку в сущность <see cref="CurrencyRates"/>
    /// </summary>
    /// <param name="rawXml">Xml-cтрока</param>
    public CbrDayRatesDto FromRawString(string rawXml)
    {
        return LoadAndParse(rawXml);
    }

    /// <summary>
    /// Парсит данные в xml элементы и переводит в предметную область
    /// </summary>
    /// <param name="loadArg">Аргумент для загрузки (путь до файла или xml-строка)</param>
    /// <returns></returns>
    /// <exception cref="FormatException">Неправильная разметка</exception>
    private static CbrDayRatesDto LoadAndParse(string loadArg)
    {
        XmlDocument doc = new();

        try
        {
            doc.LoadXml(loadArg);
            XmlElement? rootElement = doc.DocumentElement;

            if (rootElement is null || rootElement.Name is not "ValCurs")
            {
                throw new FormatException("No data inside currency rates document");
            }

            return CbrConverter.ToCbrDayRatesDto(rootElement);
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
}