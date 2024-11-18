using System.Text;
using Cbr.Application.Abstractions;

namespace HangfireServer.Specs.Fixtures.StubServices;

public class StubCbrApiService : ICbrApiService
{
    public Task<string> GetCbrDayRatesRaw(DateOnly date, CancellationToken ct)
    {
        if (!File.Exists($"../../../Fixtures/XML_{date:yyyy-MM-dd}.xml"))
        {
            throw new InvalidOperationException($"Нет курсов за дату {date:yyyy-MM-dd}");
        }

        string raw = File.ReadAllText(
            path: $"../../../Fixtures/XML_{date:yyyy-MM-dd}.xml",
            encoding: Encoding.GetEncoding("windows-1251"));

        return Task.FromResult(raw);
    }
}