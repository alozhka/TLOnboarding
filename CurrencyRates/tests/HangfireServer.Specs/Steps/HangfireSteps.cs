using Cbr.Application.Dto;
using HangfireServer.Jobs;
using HangfireServer.Jobs.Date;
using HangfireServer.Specs.Drivers;
using HangfireServer.Specs.Fixtures;
using Reqnroll;
using Xunit;

namespace HangfireServer.Specs.Steps;

[Binding]
[Scope(Tag = "hangfire")]
public sealed class HangfireSteps(HangfireServerFixture fixture)
{
    private readonly HangfireTestDriver _driver = new(fixture);
    private CbrDayRatesDto? _dayRates;

    [Then(@"за дату ""(.*)"" будут курсы:")]
    public async Task ТоЗаДатуБудутКурсы(string rawDate, DataTable table)
    {
        DateOnly date = DateOnly.Parse(rawDate);
        
        _dayRates = await _driver.GetCbrDayRates(date);
        Assert.Equal(date, DateOnly.Parse(_dayRates!.Date));

        List<CbrRateDto> expectedRates = table.CreateSet<CbrRateDto>().ToList();

        Assert.Equal(expectedRates, _dayRates.Rates);
    }

    [When("запускается импорт курсов валют из API ЦБ РФ")]
    public async Task ЗапускаетсяИмпортКурсовВалютИзApiЦбРф()
    { 
        await _driver.RunRecurringJob(ImportCbrDayRatesJob.JobId);
    }

    [BeforeScenario]
    public static void BeforeScenario()
    {
        ClockHelper.ResetUtcNow();
    }
    
    [AfterScenario]
    public static void AfterScenario()
    {
        ClockHelper.ResetUtcNow();
    }
}