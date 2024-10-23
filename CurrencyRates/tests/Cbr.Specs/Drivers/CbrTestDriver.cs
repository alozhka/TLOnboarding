using System.Text.Json;
using Cbr.Application.Dto;
using Cbr.Application.Service;

namespace Cbr.Specs.Drivers;

public class CbrTestDriver(HttpClient httpClient, CurrencyRatesService currencyRatesService)
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
    
    public void ImportDayRatesFromFile(string filepath)
    {
        currencyRatesService.ImportCbrCurrencyRates(filepath);
    }
    
    public void ImportDayRatesFromRaw(string rawXml)
    {
        currencyRatesService.ImportCbrCurrencyRatesFromRaw(rawXml);
    }
}
