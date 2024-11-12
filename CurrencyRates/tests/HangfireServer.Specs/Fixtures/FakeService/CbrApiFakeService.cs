using Cbr.Application.Abstractions;

namespace HangfireServer.Specs.Fixtures.FakeService;

public class CbrApiFakeService : ICbrApiService
{
    public Task<string> GetCbrDayRatesRaw(DateOnly date, CancellationToken ct)
    {
        const string rawFakeData = """
                                   <ValCurs Date="21.09.2021" name="Foreign Currency Market">
                                   <Valute ID="R01135">
                                      <NumCode>348</NumCode>
                                      <CharCode>HUF</CharCode>
                                      <Nominal>100</Nominal>
                                      <Name>Венгерских форинтов</Name>
                                      <Value>25,6319</Value>
                                      <VunitRate>0,256319</VunitRate>
                                   </Valute>
                                   <Valute ID="R01150">
                                      <NumCode>704</NumCode>
                                      <CharCode>VND</CharCode>
                                      <Nominal>10000</Nominal>
                                      <Name>Вьетнамских донгов</Name>
                                      <Value>40,3722</Value>
                                      <VunitRate>0,00403722</VunitRate>
                                   </Valute>
                                   <Valute ID="R01240">
                                      <NumCode>818</NumCode>
                                      <CharCode>EGP</CharCode>
                                      <Nominal>10</Nominal>
                                      <Name>Египетских фунтов</Name>
                                      <Value>19,9040</Value>
                                      <VunitRate>1,9904</VunitRate>
                                    </Valute>
                                   </ValCurs>
                                   """;

        return Task.FromResult(rawFakeData);
    }
}