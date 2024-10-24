using Cbr.Application.Abstractions;
using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRepository(CbrDbContext dbContext) : ICurrencyRepository
{
    private readonly CbrDbContext _dbContext = dbContext;

    public Task<List<string>> ListCurrencyCodesRange(List<string> currencyCodes, CancellationToken ct)
        => _dbContext.Currency.Where(c => currencyCodes.Contains(c.Code)).Select(c => c.Code).ToListAsync(ct);
}
