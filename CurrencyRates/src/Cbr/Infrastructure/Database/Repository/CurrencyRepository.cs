using Cbr.Application.Abstractions;
using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRepository(CbrDbContext dbContext) : ICurrencyRepository
{
    private readonly CbrDbContext _dbContext = dbContext;

    public void AddOrUpdateRange(List<Currency> currencies)
    {
        List<string> upcomingCurrencyCodes = currencies.Select(c => c.Code).ToList();

        List<string> incomingCurrencyCodes = _dbContext.Currency
            .Where(c => upcomingCurrencyCodes.Contains(c.Code))
            .Select(c => c.Code)
            .ToListAsync().Result;

        List<Currency> upcomingCurrenciesToUpdate = currencies.Where(c => incomingCurrencyCodes.Contains(c.Code)).ToList();
        List<Currency> upcomingCurrenciesToAdd = currencies.Where(c => !incomingCurrencyCodes.Contains(c.Code)).ToList();

        UpdateRange(upcomingCurrenciesToUpdate);
        AddRange(upcomingCurrenciesToAdd);
    }

    public void AddRange(List<Currency> currencies)
        => _dbContext.Currency.AddRange(currencies);

    public void UpdateRange(List<Currency> currencies)
        => _dbContext.Currency.UpdateRange(currencies);
}
