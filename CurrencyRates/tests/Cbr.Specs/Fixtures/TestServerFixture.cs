using Cbr.Application.Service;
using Cbr.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cbr.Specs.Fixtures;

public class TestServerFixture : IDisposable
{
    private IDbContextTransaction? _dbTransaction;
    public HttpClient HttpClient { get; }
    public CurrencyRatesService CurrencyRatesService { get; }
    public Dictionary<DateOnly, string> InMemoryDayRates = new()
    {
        {
            new DateOnly(2024, 10, 11),
            """
            <?xml version="1.0" encoding="windows-1251"?>
            <ValCurs Date="11.10.2024" name="Foreign Currency Market">
                <Valute ID="R01020A"><NumCode>944</NumCode><CharCode>HKD</CharCode><Nominal>1</Nominal>
                    <Name>Гонконгский доллар</Name><Value>12,3907</Value><VunitRate>12,3907</VunitRate>
                </Valute>
                <Valute ID="R01020A"><NumCode>944</NumCode><CharCode>JPY</CharCode><Nominal>100</Nominal>
                    <Name>Японских иен</Name><Value>64,707</Value><VunitRate>0,647076</VunitRate>
                </Valute>
            </ValCurs>   
            """
        },
        {
            new DateOnly(2024, 10, 08),
            """
            <?xml version="1.0" encoding="windows-1251"?>
            <ValCurs Date="08.10.2024" name="Foreign Currency Market">
                <Valute ID="R01010"><NumCode>036</NumCode><CharCode>AUD</CharCode><Nominal>1</Nominal>
                    <Name>Австралийский доллар</Name><Value>65,7852</Value><VunitRate>65,7852</VunitRate>
                </Valute>
                <Valute ID="R01020A"><NumCode>944</NumCode><CharCode>AZN</CharCode><Nominal>1</Nominal>
                    <Name>Азербайджанский манат</Name><Value>56,5088</Value><VunitRate>56,5088</VunitRate>
                </Valute>
            </ValCurs>   
            """
        }
    };

    public TestServerFixture()
    {
        WebApplicationFactory<Program> factory = new();
        factory = factory.WithWebHostBuilder(b =>
        {
            b.UseSolutionRelativeContentRoot("src/Service");
            b.UseEnvironment("Development");
            b.ConfigureTestServices(services =>
            {
                ServiceDescriptor descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<CbrDbContext>));
                services.AddSingleton(descriptor.ServiceType, descriptor.ImplementationFactory!);
                services.AddSingleton<CbrDbContext>();

                services.AddLogging(b => b.AddConsole().AddFilter(level => level >= LogLevel.Warning));
            });
        });
        HttpClient = factory.CreateClient();

        CbrDbContext dbContext = factory.Services.GetRequiredService<CbrDbContext>();
        _dbTransaction = dbContext.Database.BeginTransaction();

        using var scope = factory.Services.CreateScope();
        CurrencyRatesService = scope.ServiceProvider.GetRequiredService<CurrencyRatesService>();
    }
    public void Dispose()
    {
        if (_dbTransaction is not null)
        {
            _dbTransaction.Rollback();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }
        GC.SuppressFinalize(this);
    }

    ~TestServerFixture()
    {
        Dispose();
    }
}
