namespace XmlParser.Model;


using XmlAttributes = KeyValuePair<string, string>;

public class XmlElement(string name, List<XmlAttribute> attributes, string value = "")
{
    /// <summary>
    /// Название XML-элемента. Берётся из угловых скобок
    /// </summary>
    /// <example>
    /// <Element></Element> <br/> Element - название
    /// </example>
    public string Name { get; } = name;

    /// <summary>
    /// Аттрибуты XML-элемента. Берутся из угловых скобок после названия
    /// </summary>
    /// <example>
    /// <Element attr1="123" attr2="456"></Element> <br/> attr1 и attr2 - аттрибуты
    /// </example>
    public List<XmlAttribute> Attributes { get; } = attributes;

    /// <summary>
    /// Значение внутри XML-элемента, между открывающимся и закрывающимся тегом.
    /// Если тип простой, то он будет в поле <see cref="Value"/>
    /// Если тип сложный, то см. поле <see cref="Children"/>
    /// </summary>
    public string Value { get; } = value;

    /// <summary>
    /// Дети сложного XML-элемента.
    /// Если элемент является простым типом, то это поле будет пустым списком
    /// </summary>
    public List<XmlElement> Children { get; private set; } = [];
}