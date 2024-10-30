using Cbr.Application.Service;

namespace Hangfire.Jobs;

public class ImportCbrDayRatesJob(CurrencyRatesService currencyRatesService)
{
    public async Task RunAsync(DateOnly date, CancellationToken ct)
    {
        await currencyRatesService.ImportFromCbrApiAsync(date, ct);
    }
}
