using Cbr.Application.Dto;
using HangfireServer.Jobs;
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


    [When(@"я запрашиваю курсы за дату ""(.*)""")]
    public async Task ЯЗапрашиваюКурсыЗаТекущуюДату(string date)
    {
        DateOnly? dateOnly = null;
        if (!string.IsNullOrEmpty(date))
        {
            dateOnly = DateOnly.Parse(date);
        }

        _dayRates = await _driver.GetCbrDayRates(dateOnly);
    }

    [Then(@"за дату ""(.*)"" будут курсы:")]
    public void ТоЗаДатуБудутКурсы(string rawDate, DataTable table)
    {
        Assert.Equal(DateOnly.Parse(rawDate), DateOnly.Parse(_dayRates!.Date));

        List<CbrRateDto> expectedRates = table.CreateSet<CbrRateDto>().ToList();

        Assert.Equal(expectedRates, _dayRates.Rates);
    }

    [When(@"запускается импорт курсов валют к рублю")]
    public async Task WhenЗапускаетсяИмпортКурсовВалютКРублю()
    { 
        await _driver.RunRecurringJob(ImportCbrDayRatesJob.JobId);
    }
}