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

    [When(@"я запрашиваю курсы за дату {string}")]
    public async Task КогдаЯЗапрашиваюДанныеЗаДату(string date)
    {
        DateOnly? dateOnly = null;
        if (!string.IsNullOrEmpty(date))
        {
            dateOnly = DateOnly.Parse(date);
        }

        _dayRates = await _driver.GetDayRates(dateOnly);
    }

    [Then("курсы имеют дату {string}")]
    public void ТоКурсыИмеютДату(string date)
    {
        Assert.Equal(DateOnly.Parse(date), DateOnly.Parse(_dayRates!.Date));
    }

    [Then("получено курсов в количестве {int}")]
    public void ТоПолученоКурсовВКоличестве(int amount)
    {
        Assert.Equal(amount, _dayRates!.Rates.Count);
    }

    [Then("элемент №{int} курсов имеет код {string} с названием {string} и обменом {decimal}")]
    public void ТоЭлементКурсовИмеетКодСНазваниемИОбменом(int sequenceNumber, string code, string name, decimal rate)
    {
        Assert.Equal(_dayRates!.Rates[sequenceNumber - 1].CurrencyCode, code);
        Assert.Equal(_dayRates.Rates[sequenceNumber - 1].CurrencyName, name);
        Assert.Equal(_dayRates.Rates[sequenceNumber - 1].ExchangeRate, rate);
    }
}
