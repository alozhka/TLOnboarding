using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[Route("api/v1/cbr")]
[ApiController]
public class CbrController : ControllerBase
{
    [HttpGet("daily-rates")]
    public async Task<IResult> GetDayRates()
    {
        throw new NotImplementedException();
    }
}
