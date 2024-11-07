using Hangfire;
using Hangfire.Common;
using Hangfire.Helpers;
using Hangfire.Jobs;
using Hangfire.Storage;

namespace Cbr.Specs.Drivers;

public class HangfireTestDriver : IDisposable
{
    private readonly Lazy<IStorageConnection> _storageConnection = new(GetStorageConnection);
    private readonly Lazy<RecurringJobManager> _recurringJobManager = new(GetRecurringJobManager);

    private readonly List<string> _recurringJobIds = [];

    public HangfireTestDriver()
    {
        GlobalConfiguration.Configuration.UseInMemoryStorage();
    }

    public void AddCbrApiImportRecurringJob()
    {
        RecurringJob.AddOrUpdate<ImportCbrDayRatesJob>(
            ImportCbrDayRatesJob.JobId,
            s => s.RunAsync(CancellationToken.None),
            ImportCbrDayRatesJob.Cron);

        _recurringJobIds.Add(ImportCbrDayRatesJob.JobId);
    }

    public void TriggerAllRecurringJobs()
    {
        foreach (string jobId in _recurringJobIds)
        {
            RecurringJob.TriggerJob(jobId);
        }
    }

    private static IStorageConnection GetStorageConnection()
    {
        return JobStorage.Current.GetConnection()
               ?? throw new InvalidOperationException("Cannot get Hangfire job storage");
    }

    private static RecurringJobManager GetRecurringJobManager()
    {
        return new RecurringJobManager(
            JobStorage.Current,
            JobFilterProviders.Providers,
            new DefaultTimeZoneResolver(),
            Clock.UtcNow);
    }

    public void Dispose()
    {
        if (_storageConnection.IsValueCreated)
        {
            _storageConnection.Value.Dispose();
        }
    }
}