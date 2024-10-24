using Cbr.Application.Abstractions;
using Cbr.Application.Dto;
using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRateRepository(CbrDbContext dbContext) : ICurrencyRateRepository
{
    private readonly CbrDbContext _dbContext = dbContext;

    public void AddRange(List<CurrencyRate> currencyRates)
        => _dbContext.CurrencyRate.AddRange(currencyRates);

    public async Task<CbrDayRatesDto?> ListCbrDayRatesToRub(DateOnly date, CancellationToken ct)
    {
        List<CbrRateDto> rates = await _dbContext.CurrencyRate
                .Where(cr => cr.Date == date && cr.TargetCurrencyCode == "RUB")
                .Include(cr => cr.SourceCurrency)
                .OrderBy(cr => cr.SourceCurrency.Code)
                .Select(cr => new CbrRateDto(cr.SourceCurrency.Code, cr.SourceCurrency.Name, cr.ExchangeRate))
                .ToListAsync(ct);

        if (rates.Count == 0)
        {
            return null;
        }
        return new CbrDayRatesDto(date.ToString("yyyy-MM-dd"), rates);
    }

    public void SaveChanges()
        => _dbContext.SaveChanges();

    public void UpdateRange(List<CurrencyRate> currencyRates)
        => _dbContext.UpdateRange(currencyRates);
    public Task<int> SaveChangesAsync(CancellationToken ct)
        => _dbContext.SaveChangesAsync(ct);
}
