using System.Text.RegularExpressions;
using XmlParser.Model;

namespace XmlParser.Service;

public static class XmlParser
{
    private const string RegexElement = @"<(\\w+)([^>]*)>";
    
    /// <summary>
    /// Считывает xml в виде строки из файла и парсит приватным методом
    /// </summary>
    /// <param name="filepath">путь до xml файла</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException">если файл не найден</exception>
    public static XmlElement FromFile(string filepath)
    {
        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException("File not found", filepath);
        }

        string rawXml = string.Join("", File.ReadAllLines(filepath));
        return Parse(ref rawXml);
    }

    /// <summary>
    /// Парсит xml в виде строки
    /// </summary>
    /// <returns><see cref="XmlElement"/></returns>
    /// <param name="rawXml">xml в виде строки</param>
    /// <returns></returns>
    private static XmlElement Parse(ref string rawXml)
    {
        int currentIndex = rawXml.IndexOf('>') + 1;

        ParseElement(ref rawXml, ref currentIndex);
        // дальше логика последовательной обработки
    }

    
    /// <summary>
    /// Парсит значение элемента внутри угловых скобок
    /// </summary>
    /// <param name="str">строка для парсинга</param>
    /// <param name="currentIndex">текуший индекс</param>
    /// <returns></returns>
    private static XmlElement ParseElement(ref string str, ref int currentIndex)
    {
        int start = str.IndexOf('<', currentIndex);
        int end = str.IndexOf('>', start);

        string element = str.Substring(start, end - start + 1);

        Match match = Regex.Match(element, RegexElement);
        
        // думаю сделать через regex
    }
}