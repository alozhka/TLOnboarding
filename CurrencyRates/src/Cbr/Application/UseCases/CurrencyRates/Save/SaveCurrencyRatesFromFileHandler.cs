using Cbr.Infrastructure.Service;

namespace Cbr.Application.UseCases.CurrencyRates.Save;

public class SaveCurrencyRatesFromFileHandler
{
    public void Handle(SaveCurrencyRatesFromFileCommand command)
    {
        CbrXmlParser.FromFile(command.Filepath);
    }

}
