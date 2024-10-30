using Cbr.Application.Abstractions;

namespace Cbr.Infrastructure.Service;

/// <summary>
/// !!!Обязательно!!! Добавьте поддержку кодировки windows-1251.
/// Данные с Центробанка приходят именно в этой кодировке
/// </summary>
/// <param name="httpClient"></param>
public class CbrApiService(HttpClient httpClient) : ICbrApiService
{
    private readonly HttpClient _httpClient = httpClient;
    private const string _cbrDayRayRatesApiUrl = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=";
    
    public async Task<string> GetCbrDayRatesRaw(DateOnly date, CancellationToken ct)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_cbrDayRayRatesApiUrl + date.ToString("dd/MM/yyyy"), ct);

        return await response.Content.ReadAsStringAsync(ct);
    }
}
