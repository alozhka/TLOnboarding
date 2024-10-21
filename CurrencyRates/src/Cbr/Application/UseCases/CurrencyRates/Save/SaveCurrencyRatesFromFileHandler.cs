using System.Text.Json;
using Cbr.Infrastructure.Database.Repository;
using Cbr.Infrastructure.Service;
using Microsoft.Extensions.Logging;

namespace Cbr.Application.UseCases.CurrencyRates.Save;

public class SaveCurrencyRatesFromFileHandler(CurrencyRatesRepository currencyRatesRepository)
{
    private readonly CurrencyRatesRepository _currencyRatesRepository = currencyRatesRepository;

    public void Handle(SaveCurrencyRatesFromFileCommand command)
    {
        Domain.Entity.CurrencyRates rates = CbrXmlParser.FromFile(command.Filepath);

        _currencyRatesRepository.Add(rates);

        _currencyRatesRepository.SaveChanges();
    }
}
