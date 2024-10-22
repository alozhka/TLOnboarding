using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRateRepository
{
    private readonly CbrDbContext _dbContext;

    public CurrencyRateRepository(CbrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddRange(List<CurrencyRate> currencyRates)
        => _dbContext.CurrencyRate.AddRange(currencyRates);

    public Task<List<CurrencyRate>> GetAllByDate(DateOnly date, CancellationToken ct)
        => _dbContext.CurrencyRate
            .Where(cr => cr.Date == date)
            .Include(cr => cr.SourceCurrency)
            .Include(cr => cr.TargetCurrency)
            .OrderBy(cr => cr.SourceCurrency.Code)
            .ThenBy(cr => cr.TargetCurrency.Code)
            .ToListAsync(ct);

    public void SaveChanges()
        => _dbContext.SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken ct)
        => _dbContext.SaveChangesAsync(ct);
}
