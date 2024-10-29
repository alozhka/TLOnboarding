using Cbr.Application.Abstractions;
using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRepository(CbrDbContext dbContext) : ICurrencyRepository
{
    private readonly CbrDbContext _dbContext = dbContext;

    public void AddOrUpdateRange(List<Currency> currencies)
    {
        List<string> externalCurrencyCodes = currencies.Select(c => c.Code).ToList();

        List<Currency> internalCurrencies = _dbContext.Currency
            .Where(c => externalCurrencyCodes.Contains(c.Code))
            .ToListAsync().Result;
        List<string> internalCurrencyCodes = internalCurrencies.Select(c => c.Code).ToList();

        List<Currency> upcomingCurrenciesToAdd = currencies.Where(c => !internalCurrencyCodes.Contains(c.Code)).ToList();
        foreach (var currency in internalCurrencies)
        {
            currency.Name = currencies.First(c => c.Code == currency.Code).Name;
        }
        
        UpdateRange(internalCurrencies);
        AddRange(upcomingCurrenciesToAdd);
    }

    public void AddRange(List<Currency> currencies)
        => _dbContext.Currency.AddRange(currencies);

    public void UpdateRange(List<Currency> currencies)
        => _dbContext.Currency.UpdateRange(currencies);
}
