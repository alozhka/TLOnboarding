using Cbr.Application.Service;

namespace Hangfire.Jobs;

public class ImportCbrDayRatesJob(CurrencyRatesService currencyRatesService)
{
    public async Task RunAsync(CancellationToken ct)
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        await currencyRatesService.ImportFromCbrApiAsync(date, ct);
    }
}
