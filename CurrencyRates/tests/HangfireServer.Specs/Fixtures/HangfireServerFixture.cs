using Cbr.Application.Abstractions;
using Cbr.Infrastructure.Database;
using Hangfire;
using HangfireServer.Specs.Fixtures.FakeService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace HangfireServer.Specs.Fixtures;

public class HangfireServerFixture : IDisposable
{
    private IDbContextTransaction? _dbTransaction;
    public readonly IServiceScope ServiceScope;

    public HangfireServerFixture()
    {
        WebApplicationFactory<Program> factory = new();
        factory.WithWebHostBuilder(b =>
        {
            b.UseSolutionRelativeContentRoot("src/HangfireServer");
            b.UseEnvironment("Test");
            b.ConfigureServices(services =>
            {
                ReconfigureServicesToTests(services);
                services.AddLogging(loggingBuilder =>
                    loggingBuilder.AddConsole().AddFilter(level => level >= LogLevel.Warning));
            });
        });

        ServiceScope = factory.Services.CreateScope();
        DbContext dbContext = ServiceScope.ServiceProvider.GetRequiredService<CbrDbContext>();
        _dbTransaction = dbContext.Database.BeginTransaction();
    }

    private static void ReconfigureServicesToTests(IServiceCollection services)
    {
        // Убираем зависимость от внешнего API для тестов
        services.RemoveAll<IGlobalConfiguration>();
        services.AddHangfire(globalConfiguration => globalConfiguration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseInMemoryStorage());
        GlobalConfiguration.Configuration.UseInMemoryStorage();
        
        services.RemoveAll<ICbrApiService>();
        services.AddTransient<ICbrApiService, CbrApiFakeService>();
    }

    public void Dispose()
    {
        if (_dbTransaction is not null)
        {
            _dbTransaction.Rollback();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }
        
        ServiceScope.Dispose();
        GC.SuppressFinalize(this);
    }

    ~HangfireServerFixture()
    {
        Dispose();
    }
}