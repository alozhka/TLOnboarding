using Cbr.Domain.Entity;

namespace Cbr.Application.Abstractions;

public interface ICbrXmlParser
{
    CurrencyRates FromFile(string filepath);
    CurrencyRates FromRawString(string rawXml);
}
