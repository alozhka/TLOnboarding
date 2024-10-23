using Cbr.Application.Dto;
using Cbr.Specs.Drivers;
using Cbr.Specs.Fixtures;
using Reqnroll;

namespace Cbr.Specs.Steps;


[Binding]
public sealed class CbrSteps(TestServerFixture fixture)
{
    private readonly CbrTestDriver _driver = new(fixture.HttpClient, fixture.CurrencyRatesService);
    private CbrDayRatesDto _dayRates { get; set; }

    [Given(@"я импортировал данные из файла за дату {string}")]
    public void ДопустимЯИмпортировалДанныеИзФайлаЗаДату(string date)
    {
        string query = string.Format("./Fixtures/XML_{0}.xml", date);
        _driver.ImportDayRatesFromFile(query);
    }

    [When(@"я запрашиваю данные за дату {string}")]
    public async Task КогдаЯЗапрашиваюДанныеЗаДату(string date)
    {
        DateOnly? dateOnly = null;
        if (string.IsNullOrEmpty(date))
        {
            dateOnly = DateOnly.Parse(date);
        }

        _dayRates = await _driver.GetDayRates(dateOnly);
    }
}
