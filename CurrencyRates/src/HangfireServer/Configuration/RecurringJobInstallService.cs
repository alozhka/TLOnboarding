using Hangfire;
using HangfireServer.Jobs;

namespace HangfireServer.Configuration;

public class RecurringJobInstallService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(InstallRecurringJobs, stoppingToken);
    }

    private static void InstallRecurringJobs()
    {
        AddRecurringJob<ImportCbrDayRatesJob>(ImportCbrDayRatesJob.JobId, ImportCbrDayRatesJob.Cron);
    }

    private static void AddRecurringJob<T>(string jobId, string cron) where T : IJob
    {
        RecurringJob.AddOrUpdate<T>(
            jobId,
            x => x.Run(null!, CancellationToken.None),
            cron);
    }
}