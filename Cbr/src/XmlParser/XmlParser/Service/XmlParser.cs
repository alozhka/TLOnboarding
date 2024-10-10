using System.Text.RegularExpressions;
using XmlParser.Model;

namespace XmlParser.Service;

public class XmlParser
{
    /// <summary>
    /// Регулярное выражение для извлечения названия и аттрибутов у xml-элемента
    /// </summary>
    private const string RegexElement = @"<(\\w+)([^>]*)>";
    
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

        _rawXml = string.Join("", File.ReadAllLines(filepath));
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
    }

    
    /// <summary>
    /// Парсит значение элемента внутри угловых скобок
    /// </summary>
    /// <returns>Элемент без детей</returns>
    private XmlElement ParseElement()
    {
        int start = _rawXml.IndexOf('<', _currentIndex);
        int end = _rawXml.IndexOf('>', start);

        string element = _rawXml.Substring(start, end - start + 1);

        Match match = Regex.Match(element, RegexElement);
        
        // думаю сделать через regex
    }
}