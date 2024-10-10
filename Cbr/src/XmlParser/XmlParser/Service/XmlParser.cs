using XmlParser.Model;

namespace XmlParser.Service;

public class XmlParser
{
    /// <summary>
    /// Xml в виде строки
    /// </summary>
    private string _rawXml = "";

    /// <summary>
    /// Текущий индекс во время обработки <see cref="_rawXml"/>
    /// </summary>
    private int _currentIndex = 0;

    /// <summary>
    /// Считывает xml в виде строки из файла и парсит приватным методом
    /// </summary>
    /// <param name="filepath">путь до xml файла</param>
    /// <exception cref="FileNotFoundException">если файл не найден</exception>
    public XmlElement FromFile(string filepath)
    {
        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException("File not found", filepath);
        }

        _rawXml = File.ReadAllText(filepath);
        return Parse();
    }


    /// <summary>
    /// Задаёт значение для полей класса и парсит xml
    /// </summary>
    /// <returns><see cref="XmlElement"/> Главный элемент с детьми</returns>
    /// <param name="rawXml">xml в виде строки</param>
    public XmlElement FromRawString(string rawXml)
    {
        _rawXml = rawXml;
        return Parse();
    }

    /// <summary>
    /// Парсит xml в виде строки
    /// </summary>
    /// <returns><see cref="XmlElement"/> Главный элемент с детьми</returns>
    /// <param name="rawXml">xml в виде строки</param>
    private XmlElement Parse()
    {
        _currentIndex = _rawXml.IndexOf('>') + 1;

        XmlElement el = ParseElement();
        // дальше логика последовательной обработки
        return el;
    }


    /// <summary>
    /// Парсит значение элемента внутри угловых скобок
    /// </summary>
    /// <returns>Элемент без детей</returns>
    private XmlElement ParseElement()
    {
        int start = _rawXml.IndexOf('<', _currentIndex);
        int end = _rawXml.IndexOf('>', start);
        string element = _rawXml.Substring(start + 1, end - start - 1);
        string[] entries = element.Split(' ', StringSplitOptions.TrimEntries);

        _currentIndex = end;
        List<XmlAttribute> attributes = entries[1..].Select(str =>
            {
                string[] keyValue = str.Split('=', StringSplitOptions.TrimEntries);
                return new XmlAttribute(keyValue[0], keyValue[1][1..^1]);
            })
            .ToList();

        return new XmlElement(entries[0], attributes);
    }
}