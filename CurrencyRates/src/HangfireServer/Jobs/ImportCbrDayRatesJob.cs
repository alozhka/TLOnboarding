using Cbr.Application.Service;
using Hangfire.Server;
using HangfireServer.Jobs.Date;

namespace HangfireServer.Jobs;

public class ImportCbrDayRatesJob(CurrencyRatesService currencyRatesService) : IJob
{
    public const string JobId = "Cbr.DayRates.DailyImport";

    /// <summary>
    /// Отображает время запуска задачи.
    /// 0 - запуск в начале часа (00 минут)
    /// 6,15 - в 6 и 15 часов по UTC => (+ 3 часа) => 9,18 по Москве
    /// * - каждый день
    /// * - каждый месяц
    /// * - каждый день недели
    /// </summary>
    public const string Cron = "0 6,15 * * *";

    public async Task Run(PerformContext performContext, CancellationToken ct)
    {
        DateOnly date = DateOnly.FromDateTime(Clock.UtcNow());
        await currencyRatesService.ImportFromCbrApiAsync(date, ct);
    }
}