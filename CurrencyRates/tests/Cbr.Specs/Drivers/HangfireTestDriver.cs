using Hangfire;
using Hangfire.Common;
using Hangfire.Storage;
using HangfireServer.Helpers.Date;
using HangfireServer.Jobs;

namespace Cbr.Specs.Drivers;

public class HangfireTestDriver : IDisposable
{
    private readonly Lazy<IStorageConnection> _storageConnection = new(GetStorageConnection);
    private readonly Lazy<RecurringJobManager> _recurringJobManager = new(GetRecurringJobManager);

    private readonly List<string> _recurringJobIds = [];

    public HangfireTestDriver()
    {
    }

    public void AddCbrApiImportRecurringJob()
    {
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

        GC.SuppressFinalize(this);
    }
}