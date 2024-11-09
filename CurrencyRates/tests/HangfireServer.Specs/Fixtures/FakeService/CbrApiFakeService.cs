using Cbr.Application.Abstractions;

namespace HangfireServer.Specs.Fixtures.FakeService;

public class CbrApiFakeService : ICbrApiService
{
    public Task<string> GetCbrDayRatesRaw(DateOnly date, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}