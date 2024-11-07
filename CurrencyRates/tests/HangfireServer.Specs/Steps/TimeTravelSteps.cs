using HangfireServer.Specs.Helpers.Date;
using Reqnroll;

namespace HangfireServer.Specs.Steps;

[Binding]
[Scope(Tag = "time_travel")]
public class TimeTravelSteps
{
    [When("по Москве наступает время {string}")]
    [Given("по Москве было время {string}")]
    public static void ПоМосквеНаступилоВремя(string dateTime)
    {
        ClockHelper.SetUtcNow(dateTime);
    }
}