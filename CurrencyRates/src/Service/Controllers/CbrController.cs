using Cbr.Application.Service;
using Cbr.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[Route("api/v1/cbr")]
[ApiController]
public class CbrController : ControllerBase
{
    [HttpGet("daily-rates")]
    public async Task<ActionResult<List<CurrencyRate>>> GetDayRates(
        [FromQuery] DateOnly? requestDate,
        [FromServices] CurrencyRatesService ratesService,
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
        List<CurrencyRate> rates = await ratesService.ListDayRatesByDate(date, ct);

        if (rates.Count == 0)
        {
            return NotFound();
        }
        
        return rates;
    }
}
