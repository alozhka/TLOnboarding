using System.Text.Json;
using Cbr.Application.Dto;

namespace Cbr.Specs.Drivers;

public class CbrTestDriver(HttpClient httpClient)
{
    public async Task<CbrDayRatesDto> GetDayRates(DateOnly? requestDate = null)
    {
        string query = "api/v1/cbr/daily-rates";
        if (requestDate != null)
        {
            query += "/requestDate=" + requestDate?.ToString("yyyy-MM-dd");
        }
        
        HttpResponseMessage response = await httpClient.GetAsync(query);

        string content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CbrDayRatesDto>(content) 
            ?? throw new ArgumentException($"Unexpected JSON response: {content}");
    }
}
