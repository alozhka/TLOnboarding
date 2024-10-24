using Cbr.Application.Dto;

namespace Cbr.Application.Abstractions;

public interface ICbrXmlParser
{
    CbrDayRatesDto FromFile(string filepath);
}
