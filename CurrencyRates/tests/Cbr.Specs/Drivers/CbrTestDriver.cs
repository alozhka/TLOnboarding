using Cbr.Application.Dto;
using Cbr.Application.Service;
using Newtonsoft.Json;

namespace Cbr.Specs.Drivers;

public class CbrTestDriver(HttpClient httpClient, CurrencyRatesService currencyRatesService)
{
    public async Task<CbrDayRatesDto> GetDayRates(DateOnly? requestDate = null)
    {
        string query = "api/v1/cbr/daily-rates";
        if (requestDate != null)
        {
            query += "?requestDate=" + requestDate?.ToString("yyyy-MM-dd");
        }

        HttpResponseMessage response = await httpClient.GetAsync(query);

        await EnsureSuccessResponse(response);
        string content = await response.Content.ReadAsStringAsync();
        CbrDayRatesDto dto = JsonConvert.DeserializeObject<CbrDayRatesDto>(content)
            ?? throw new ArgumentException($"Unexpected JSON response: {content}");
        return dto;
    }

    public void ImportDayRatesFromFile(string filepath)
    {
        currencyRatesService.ImportCbrCurrencyRates(filepath);
    }

    public void ImportDayRatesFromRaw(string rawXml)
    {
        currencyRatesService.ImportCbrCurrencyRatesFromRaw(rawXml);
    }

    private static async Task EnsureSuccessResponse(HttpResponseMessage responseMessage)
    {
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            Assert.Fail($"HTTP status code {responseMessage.StatusCode}: {content}");
        }
    }
}
