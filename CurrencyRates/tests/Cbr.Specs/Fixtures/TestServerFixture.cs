using Cbr.Application;
using Cbr.Application.Service;
using Cbr.Infrastructure;
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

    public TestServerFixture()
    {
        WebApplicationFactory<Program> factory = new();
        factory = factory.WithWebHostBuilder(b =>
        {
            b.UseSolutionRelativeContentRoot("src/Service");
            b.UseEnvironment("Development");
            b.ConfigureTestServices(services =>
            {
                services.AddCbrApplication();
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
