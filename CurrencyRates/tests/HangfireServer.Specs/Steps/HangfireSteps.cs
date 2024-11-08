using System.Globalization;
using Cbr.Application.Dto;
using Cbr.Application.Service;
using HangfireServer.Specs.Drivers;
using HangfireServer.Specs.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace HangfireServer.Specs.Steps;

[Binding]
[Scope(Tag = "hangfire")]
public sealed class HangfireSteps(HangfireServerFixture fixture)
{
    private readonly HangfireTestDriver _driver = new(fixture);
    private CbrDayRatesDto? _dayRatesDto;


    [When(@"я запрашиваю курсы за дату ""(.*)""")]
    public async Task WhenЯЗапрашиваюКурсыЗаТекущуюДату(string date)
    {
        _dayRatesDto = await _driver.GetCbrDayRates();
    }
}