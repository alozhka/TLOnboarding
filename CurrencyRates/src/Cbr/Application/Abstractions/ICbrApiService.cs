namespace Cbr.Application.Abstractions;

public interface ICbrApiService
{
    Task<string> GetCbrDayRatesRaw(DateOnly date, CancellationToken ct);
}
