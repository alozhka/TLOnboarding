using Reqnroll;

namespace HangfireServer.Specs.Drivers;

[Binding]
public sealed class HangfireSteps
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
