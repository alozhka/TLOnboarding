using Cbr.Application.Dto;
using Cbr.Application.Service;
using Cbr.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[Route("api/v1/cbr")]
[ApiController]
public class CbrController : ControllerBase
{
    [HttpGet("daily-rates")]
    public async Task<ActionResult<CbrDayRatesDto>> GetDayRates(
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
        CbrDayRatesDto? rates = await ratesService.ListDayRatesByDate(date, ct);

        if (rates  is null)
        {
            return NotFound();
        }
        
        return rates;
    }
}
