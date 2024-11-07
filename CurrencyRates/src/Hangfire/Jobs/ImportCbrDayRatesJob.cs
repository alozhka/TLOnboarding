using Cbr.Application.Service;
using Hangfire.Server;

namespace Hangfire.Jobs;

public class ImportCbrDayRatesJob(CurrencyRatesService currencyRatesService) : IJob
{
    public const string JobId = "Cbr.DayRates.DailyImport";

    /// <summary>
    /// Отображает время запуска задачи.
    /// 0 - запуск в начале часа (00 минут)
    /// 9,18 - в 9 и 18 часов
    /// * - каждый день
    /// * - каждый месяц
    /// * - каждый день недели
    /// </summary>
    public const string Cron = "0 9,18 * * *";

    public async Task Run(PerformContext performContext, CancellationToken ct)
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        await currencyRatesService.ImportFromCbrApiAsync(date, ct);
    }
}