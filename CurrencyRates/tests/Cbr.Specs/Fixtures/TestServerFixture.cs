using Cbr.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Cbr.Specs.Fixtures;

public class TestServerFixture : IDisposable
{
    private IDbContextTransaction? _dbTransaction;
    public HttpClient HttpClient { get; }

    public TestServerFixture()
    {
        WebApplicationFactory<Program> factory = new();
        factory = factory.WithWebHostBuilder(b =>
        {
            b.UseSolutionRelativeContentRoot("cbr-frontend");
            b.UseEnvironment("Development");
            b.ConfigureTestServices(services =>
            {
                ServiceDescriptor descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<CbrDbContext>));
                services.AddSingleton(descriptor.ServiceType, descriptor.ImplementationFactory!);
                services.AddSingleton<CbrDbContext>();

                services.AddLogging(b => b.AddConsole().AddFilter(level => level >= LogLevel.Warning));
            });
        });

        CbrDbContext dbContext = factory.Services.GetRequiredService<CbrDbContext>();
        _dbTransaction = dbContext.Database.BeginTransaction();

        HttpClient = factory.CreateClient();
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
