using Cbr.Application.Abstractions;
using Cbr.Application.UseCases.CurrencyRates.GetDay;
using Cbr.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[Route("api/v1/cbr")]
[ApiController]
public class CbrController : ControllerBase
{
    [HttpGet("daily-rates")]
    public async Task<IResult> GetDayRates(
        [FromQuery] DateOnly? requestDate,
        [FromServices] GetDayCurrencyRatesHandler handler,
        CancellationToken ct)
    {
        DateOnly date;
        if (requestDate is null)
        {
            date = DateOnly.FromDateTime(DateTime.UtcNow);
        }
        else
        {
            date = (DateOnly) requestDate;
        }
        Result<CurrencyRates> result = await handler.Handle(new GetDayCurrencyRatesCommand(date), ct);

        if (result.IsFailure)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(result.Value);
    }
}
