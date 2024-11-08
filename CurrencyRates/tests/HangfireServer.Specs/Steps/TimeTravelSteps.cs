using Reqnroll;

namespace HangfireServer.Specs.Steps;

[Binding]
[Scope(Tag = "time_travel")]
public class TimeTravelSteps
{
    [Given(@"по Москве было время ""(.*)""")]
    [When(@"по Москве наступает время ""(.*)""")]
    public static void ПоМосквеНаступаетВремя(string p0)
    {
        ScenarioContext.StepIsPending();
    }
}