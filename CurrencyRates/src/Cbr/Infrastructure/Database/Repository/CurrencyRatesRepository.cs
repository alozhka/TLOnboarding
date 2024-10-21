using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRatesRepository
{
    private readonly CbrDbContext _dbContext;

    public CurrencyRatesRepository(CbrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CurrencyRates currencyRates)
        => _dbContext.CurrencyRates.Add(currencyRates);

    public Task<CurrencyRates?> GetByDate(DateOnly date, CancellationToken ct)
        => _dbContext.CurrencyRates
            .Where(cr => cr.Date == date)
            .Include(cr => cr.Currencies.OrderByDescending(c => c.CharCode))
            .FirstOrDefaultAsync(ct);

    public Task<int> SaveChangesAsync(CancellationToken ct)
        => _dbContext.SaveChangesAsync(ct);
}
