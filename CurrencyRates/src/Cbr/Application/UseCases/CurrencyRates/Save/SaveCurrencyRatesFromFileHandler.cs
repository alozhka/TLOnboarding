using Cbr.Infrastructure.Database.Repository;
using Cbr.Infrastructure.Service;

namespace Cbr.Application.UseCases.CurrencyRates.Save;

public class SaveCurrencyRatesFromFileHandler
{
    private readonly CurrencyRatesRepository _currencyRatesRepository;

    public SaveCurrencyRatesFromFileHandler(CurrencyRatesRepository currencyRatesRepository)
    {
        _currencyRatesRepository = currencyRatesRepository;
    }

    public void Handle(SaveCurrencyRatesFromFileCommand command)
    {
        Domain.Entity.CurrencyRates rates = CbrXmlParser.FromFile(command.Filepath);

        _currencyRatesRepository.Add(rates);

        _currencyRatesRepository.SaveChanges();
    }
}
