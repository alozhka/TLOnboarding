using Cbr.Domain.Entity;

namespace Cbr.Application.Abstractions;

public interface ICbrXmlParser
{
    List<CurrencyRate> FromFile(string filepath);
}
