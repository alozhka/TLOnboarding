using Cbr.Specs.Drivers;
using Reqnroll;

namespace Cbr.Specs.Steps;

[Binding]
public sealed class HangfireSpecs
{
    private readonly HangfireTestDriver _driver = new();


    [Given("я создал задачу автоматического иморта курсов из ЦБ РФ")]
    public void ЯСоздалЗадачуАвтоматическогоИмортаИзЦбрф()
    {
        _driver.AddCbrApiImportRecurringJob();
    }

    [When("наступает нужное время")]
    public void НаступаетНужноеВремя()
    {
        _driver.TriggerAllRecurringJobs();
    }
}
