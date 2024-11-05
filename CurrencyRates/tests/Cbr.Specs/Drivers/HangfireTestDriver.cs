using Hangfire;
using Hangfire.InMemory;
using Hangfire.Jobs;
using Hangfire.Storage;

namespace Cbr.Specs.Drivers;

public class HangfireTestDriver : IDisposable
{
    public BackgroundJobServer JobServer { get; }
    public JobStorage JobStorage { get; }

    private readonly List<string> _recurringJobIds = [];

    public HangfireTestDriver()
    {
        GlobalConfiguration.Configuration.UseInMemoryStorage();
        JobStorage = new InMemoryStorage();
        JobServer = new BackgroundJobServer();

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
    
    public void Dispose()
    {
        JobServer.Dispose();
    }
}