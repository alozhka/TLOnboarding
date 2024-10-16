using Cbr.Domain.Entity;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRatesRepository
{
    private readonly CbrDbContext _dbContext;

    public CurrencyRatesRepository(CbrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CurrencyRates currencyRates)
    {
        _dbContext.CurrencyRates.Add(currencyRates);
    }
}
