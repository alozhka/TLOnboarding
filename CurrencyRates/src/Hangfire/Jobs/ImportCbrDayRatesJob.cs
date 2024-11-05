using Cbr.Application.Service;

namespace Hangfire.Jobs;

public class ImportCbrDayRatesJob(CurrencyRatesService currencyRatesService)
{
    public const string JobId = "Import day rates from cbr api";
    
    /// <summary>
    /// Отображает время запуска задачи.
    /// 0 - запуск в начале часа (00 минут)
    /// 9,18 - в 9 и 18 часов
    /// * - каждый жень
    /// * - каждый месяц
    /// * - каждый день недели
    /// </summary>
    public const string Cron = "0 9,18 * * *";
    
    public async Task RunAsync(CancellationToken ct)
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        await currencyRatesService.ImportFromCbrApiAsync(date, ct);
    }
}
