using Cbr.Application.Dto;
using Cbr.Application.Service;
using Hangfire;
using Hangfire.Common;
using Hangfire.Storage;
using HangfireServer.Jobs.Date;
using HangfireServer.Specs.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HangfireServer.Specs.Drivers;

public class HangfireTestDriver(HangfireServerFixture fixture) : IDisposable
{
    private readonly Lazy<IStorageConnection> _storageConnection = new(GetStorageConnection);
    private readonly Lazy<RecurringJobManager> _recurringJobManager = new(GetRecurringJobManager);

    private readonly CurrencyRatesService _currencyRatesService =
        fixture.ServiceScope.ServiceProvider.GetRequiredService<CurrencyRatesService>();

    private readonly List<string> _affectedRecurringJobIds = [];

    public async Task<CbrDayRatesDto?> GetCbrDayRates(DateOnly? requestDate = null)
    {
        DateOnly date = requestDate ?? DateOnly.FromDateTime(DateTime.Now);
        CbrDayRatesDto? dto = await _currencyRatesService.ListDayRatesByDate(date, CancellationToken.None);

        return dto;
    }

    private static async Task EnsureSuccessResponse(HttpResponseMessage responseMessage)
    {
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            Assert.Fail($"HTTP status code {responseMessage.StatusCode}: {content}");
        }
    }

    public async Task RunRecurringJob(string jobId)
    {
        _affectedRecurringJobIds.Add(jobId);

        //TODO: попробовать добавить метод на начало и конец сценария
        _recurringJobManager.Value.TriggerJob(jobId);

        BackgroundJobStatus state = await WaitRecurringJobFinished(jobId);
        if (!state.IsSucceeded)
        {
            Exception innerException = state.LoadException();
            throw new InvalidOperationException(
                $"Recurring job {jobId} failed: {innerException.Message}",
                innerException);
        }
    }

    private async Task<BackgroundJobStatus> WaitRecurringJobFinished(string recurringJobId)
    {
        IStorageConnection connection = _storageConnection.Value;
        string jobId = RecurringJobStatus.Fetch(connection, recurringJobId).LastJobId;
        BackgroundJobStatus jobStatus = BackgroundJobStatus.Fetch(connection, jobId);
        while (!jobStatus.IsFinal)
        {
            await Task.Delay(1);
            jobStatus = BackgroundJobStatus.Fetch(connection, jobId);
        }

        return jobStatus;
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