using Cbr.Application.Abstractions;
using Cbr.Application.Dto;
using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database.Repository;

public class CurrencyRateRepository(CbrDbContext dbContext) : ICurrencyRateRepository
{
    private readonly CbrDbContext _dbContext = dbContext;

    public void AddOrUpdateRange(List<CurrencyRate> currencyRates)
    {
        /* List<string> externalSourceCodes = currencyRates.Select(cr => cr.SourceCurrencyCode).ToList();
        List<string> externalTargetCodes = currencyRates.Select(cr => cr.TargetCurrencyCode).ToList();
        List<string> externalDates = currencyRates.Select(cr => cr.TargetCurrencyCode).ToList();

        List<CurrencyRate> ratesInDb = _dbContext.CurrencyRate
            .Where(cr => externalSourceCodes.Contains(cr.SourceCurrencyCode) && externalTargetCodes.Contains(cr.TargetCurrencyCode))
            .ToListAsync().Result;
        List<string> SourceCodesInDb = ratesInDb.Select(cr => cr.SourceCurrencyCode).ToList();
        List<string> TargetCodesInDb = ratesInDb.Select(cr => cr.TargetCurrencyCode).ToList();

        List<CurrencyRate> ratesToAdd = currencyRates
            .Where(cr => !SourceCodesInDb.Contains(cr.SourceCurrencyCode) && !TargetCodesInDb.Contains(cr.TargetCurrencyCode))
            .ToList();

        foreach (CurrencyRate rate in ratesInDb)
        {
            currencyRates.
        } */
        _dbContext.CurrencyRate.AddRange(currencyRates);
    }

    public async Task<CbrDayRatesDto?> ListCbrDayRatesToRub(DateOnly date, CancellationToken ct)
    {
        List<CbrRateDto> rates = await _dbContext.CurrencyRate
                .Where(cr => cr.Date == date && cr.TargetCurrencyCode == "RUB")
                .OrderBy(cr => cr.SourceCurrencyCode)
                .Select(cr => new CbrRateDto(cr.SourceCurrencyCode, "", cr.ExchangeRate))
                .ToListAsync(ct);

        if (rates.Count == 0)
        {
            return null;
        }

        List<string> currencyCodes = rates.Select(c => c.CurrencyCode).ToList();

        List<string> currencyNames = await _dbContext.Currency
            .Where(c => currencyCodes.Contains(c.Code))
            .OrderBy(c => c.Code)
            .Select(c => c.Name)
            .ToListAsync(ct);

        for (int i = 0; i < rates.Count; i++)
        {
            rates[i] = new CbrRateDto(rates[i].CurrencyCode, currencyNames[i], rates[i].ExchangeRate);
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
