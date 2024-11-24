using Cbr.Application.Dto;
using Cbr.Specs.Drivers;
using Cbr.Specs.Fixtures;
using Reqnroll;

namespace Cbr.Specs.Steps;


[Binding]
public sealed class CbrSteps(TestServerFixture fixture)
{
    private readonly CbrTestDriver _driver = new(fixture.HttpClient, fixture.CurrencyRatesService);
    private CbrDayRatesDto? _dayRates;
    private readonly Dictionary<DateOnly, string> _inMemoryDayRates = fixture.InMemoryDayRates;

    /*
    Пусть
    */
    [Given(@"я импортировал курсы из файла за дату {string}")]
    public void ПустьЯИмпортировалДанныеИзФайлаЗаДату(string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            date = "daily";
        }

        string query = string.Format("../../../Fixtures/XML_{0}.xml", date);
        _driver.ImportDayRatesFromFile(query);
    }

    [Given(@"я импортировал курсы из памяти за дату {string}")]
    public void ПустьЯИмпортировалДанныеИзПамятиЗаДату(string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            date = "daily";
        }

        string rawXml = _inMemoryDayRates[DateOnly.Parse(date)];
        _driver.ImportDayRatesFromRaw(rawXml);
    }

    /*
    Когда
    */
    [When(@"я запрашиваю курсы за дату {string}")]
    public async Task КогдаЯЗапрашиваюКурсыЗаДату(string date)
    {
        DateOnly? dateOnly = null;
        if (!string.IsNullOrEmpty(date))
        {
            dateOnly = DateOnly.Parse(date);
        }

        _dayRates = await _driver.GetDayRates(dateOnly);
    }
    
    [When("я запрашиваю курсы за текущую дату")]
    public async Task КогдаЯЗапрашиваюКурсыТекущуюЗаДату()
    {
        _dayRates = await _driver.GetDayRates();
    }

    /*
    Тогда
    */
    [Then("за дату {string} будут курсы:")]
    public void ТоЗаДатуБудутКурсы(string rawDate, DataTable table)
    {
        Assert.Equal(DateOnly.Parse(rawDate), DateOnly.Parse(_dayRates!.Date));

        List<CbrRateDto> expectedRates = table.CreateSet<CbrRateDto>().ToList();

        Assert.Equal(expectedRates, _dayRates.Rates);
    }

    [Then("за текущую дату будут курсы:")]
    public void ЗаТекущуюДатуБудутКурсы(DataTable table)
    {
        
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now), DateOnly.Parse(_dayRates!.Date));

        List<CbrRateDto> expectedRates = table.CreateSet<CbrRateDto>().ToList();

        Assert.Equal(expectedRates, _dayRates.Rates);
    }
}
