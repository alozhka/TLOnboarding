using System.Text.Json;
using Cbr.Infrastructure.Database.Repository;
using Cbr.Infrastructure.Service;
using Microsoft.Extensions.Logging;

namespace Cbr.Application.UseCases.CurrencyRates.Save;

public class SaveCurrencyRatesFromFileHandler
{
    private readonly CurrencyRatesRepository _currencyRatesRepository;
    private readonly ILogger<SaveCurrencyRatesFromFileHandler> _logger;

    public SaveCurrencyRatesFromFileHandler(CurrencyRatesRepository currencyRatesRepository,
        ILogger<SaveCurrencyRatesFromFileHandler> logger)
    {
        _currencyRatesRepository = currencyRatesRepository;
        _logger = logger;
    }

    public void Handle(SaveCurrencyRatesFromFileCommand command)
    {
        Domain.Entity.CurrencyRates rates = CbrXmlParser.FromFile(command.Filepath);

        _logger.LogInformation("Распарсили данные: {0}", JsonSerializer.Serialize(rates));
        _currencyRatesRepository.Add(rates);

        _currencyRatesRepository.SaveChanges();
    }
}
