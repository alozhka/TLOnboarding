using XmlParser.Model;

namespace XmlParser.Service;

public static class XmlParser
{
    /// <summary>
    /// Xml в виде строки
    /// </summary>
    private static string _rawXml = "";

    /// <summary>
    /// Текущий индекс во время обработки <see cref="_rawXml"/>
    /// </summary>
    private static int _currentIndex = 0;

    /// <summary>
    /// Инициализирует значения из файла и парсит xml
    /// </summary>
    /// <param name="filepath">путь до xml файла</param>
    /// <exception cref="FileNotFoundException">если файл не найден</exception>
    public static XmlElement FromFile(string filepath)
    {
        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException("File not found", filepath);
        }

        _rawXml = File.ReadAllText(filepath);
        _currentIndex = 0;

        return Parse();
    }


    /// <summary>
    /// Инициализирует значения и парсит xml
    /// </summary>
    /// <returns><see cref="XmlElement"/>Главный элемент с детьми</returns>
    /// <param name="rawXml">xml в виде строки</param>
    public static XmlElement FromRawString(string rawXml)
    {
        _rawXml = rawXml;
        _currentIndex = 0;

        return Parse();
    }

    /// <summary>
    /// Парсит xml в виде строки
    /// </summary>
    /// <returns><see cref="XmlElement"/> Главный элемент с детьми</returns>
    /// <param name="rawXml">xml в виде строки</param>
    private static XmlElement Parse()
    {
        _currentIndex = _rawXml.IndexOf('>') + 1;

        XmlElement first = ParseElement();

        Stack<XmlElement> elements = new();

        elements.Push(first);
        while (elements.Count > 0)
        {
            if (TryGetPlainValue(out string plainValue))
            {
                elements.Peek().Value = plainValue;
                continue;
            }

            XmlElement el = ParseElement();
            if (IsClosedElement(el))
            {
                if (elements.Peek().Name != el.Name[1..])
                {
                    throw new FormatException("Invalid XML nesting");
                }

                elements.Pop();
            }
            else
            {
                elements.Peek().Children.Add(el);
                elements.Push(el);
            }
        }

        return first;
    }


    /// <summary>
    /// Парсит значение элемента внутри угловых скобок
    /// </summary>
    /// <returns>Элемент без детей</returns>
    private static XmlElement ParseElement()
    {
        int start = _rawXml.IndexOf('<', _currentIndex);
        int end = _rawXml.IndexOf('>', start);
        // Значение внутри угловых скобок <>
        string element = _rawXml.Substring(start + 1, end - start - 1);

        List<string> entries = SplitWithNoAdditionalSpaces(element);
        _currentIndex = end + 1;

        List<XmlAttribute> attributes = entries[1..].Select(str =>
            {
                string[] keyValue = str.Split('=', StringSplitOptions.TrimEntries);
                return new XmlAttribute(keyValue[0], string.Join("", keyValue[1..])[1..^1]);
            })
            .ToList();

        return new XmlElement(entries[0], attributes);
    }

    /// <summary>
    /// Пытается взять простое значение. Состояние полей не изменяет.
    /// Если следующий идёт тэг, то возвращается false.
    /// </summary>
    /// <param name="plainValue"></param>
    /// <returns></returns>
    private static bool TryGetPlainValue(out string plainValue)
    {
        int startIndex = _rawXml.IndexOf('<', _currentIndex);
        plainValue = _rawXml.Substring(_currentIndex, startIndex - _currentIndex).Trim();
        _currentIndex = startIndex;
        return !string.IsNullOrEmpty(plainValue);
    }

    /// <summary>
    /// Разделяет строку по пробелам.
    /// Игнорирует множество пробелов, идущих подряд, оставляя только один.
    /// Игнорирует пробелы внутри кавычек. 
    /// </summary>
    /// <returns></returns>
    private static List<string> SplitWithNoAdditionalSpaces(string str)
    {
        List<string> words = [];
        string word = "";
        bool insideQuotes = false;

        foreach (var ch in str)
        {
            if (ch == '"')
            {
                insideQuotes = !insideQuotes;
            }

            if (!insideQuotes && char.IsWhiteSpace(ch))
            {
                if (word.Length > 0)
                {
                    words.Add(word);
                    word = "";
                }
            }
            else
            {
                word += ch;
            }
        }

        if (word.Length > 0)
        {
            words.Add(word);
        }

        return words;
    }

    /// <summary>
    /// Проверяет, закрытый ли тэг у считавшегося элемента
    /// </summary>
    /// <param name="el">Проверяемый xml-элемент</param>
    /// <returns></returns>
    private static bool IsClosedElement(XmlElement el)
    {
        return el.Name[0] == '/';
    }
}