using Cbr.Application.Dto;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using HangfireServer.Specs.Fixtures;
using HangfireServer.Specs.Helpers.Date;
using Newtonsoft.Json;
using Xunit;

namespace HangfireServer.Specs.Drivers;

public class HangfireTestDriver(HangfireServerFixture fixture) : IDisposable
{
    private readonly Lazy<IStorageConnection> _storageConnection = new(GetStorageConnection);
    private readonly Lazy<RecurringJobManager> _recurringJobManager = new(GetRecurringJobManager);

    private readonly HttpClient _httpClient = fixture.HttpClient;
    private readonly List<string> _affectedRecurringJobIds = [];

    public async Task<CbrDayRatesDto> GetCbrDayRates(DateOnly? requestDate = null)
    {
        string query = "api/v1/cbr/daily-rates";
        if (requestDate != null)
        {
            query += "?requestDate=" + requestDate?.ToString("yyyy-MM-dd");
        }

        HttpResponseMessage response = await _httpClient.GetAsync(query);

        await EnsureSuccessResponse(response);
        string content = await response.Content.ReadAsStringAsync();
        CbrDayRatesDto dto = JsonConvert.DeserializeObject<CbrDayRatesDto>(content)
                             ?? throw new ArgumentException($"Unexpected JSON response: {content}");
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

        _recurringJobManager.Value.TriggerJob(jobId);

        StateData? stateData = _storageConnection.Value.GetStateData(jobId);
        if (stateData is null)
        {
            throw new InvalidOperationException($"Cannot find job with id='{jobId}'");
        }
        
        while (stateData.Name != SucceededState.StateName && stateData.Name != DeletedState.StateName)
        {
            await Task.Delay(1);
            stateData = _storageConnection.Value.GetStateData(jobId);
        }

        if (stateData.Name == SucceededState.StateName)
        {
            throw new InvalidOperationException(
                $"Recurring job {jobId} failed: здесь заглушка для будущих внутренних исключений");
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