using Cbr.Application.Abstractions;
using Cbr.Infrastructure.Database;
using Hangfire;
using HangfireServer.Specs.Fixtures.StubServices;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SpecsLibrary.Fixtures;

namespace HangfireServer.Specs.Fixtures;

public class HangfireServerFixture : IDisposable
{
    private IDbContextTransaction? _dbTransaction;
    public readonly IServiceScope ServiceScope;


    public HangfireServerFixture()
    {
        CustomWebApplicationFactory<Program> factory = new(configureServices: services =>
        {
            // для Hangfire делаем хранилище в ОЗУ, так как тяжело будет сделать откаты при работе с СУБД
            // конфигурация такая же
            services.RemoveAll<IGlobalConfiguration>();
            services.AddHangfire(globalConfiguration =>
            {
                globalConfiguration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseInMemoryStorage();
            });

            // фейковый API сервис ЦБ РФ
            services.RemoveAll<ICbrApiService>();
            services.AddTransient<ICbrApiService, StubCbrApiService>();
        });

        factory.WithWebHostBuilder(b =>
        {
            b.UseSolutionRelativeContentRoot("src/HangfireServer");
            b.ConfigureTestServices(services =>
            {
                services.AddLogging(loggingBuilder =>
                    loggingBuilder.AddConsole().AddFilter(level => level >= LogLevel.Warning));
            });
        });
        factory.CreateClient();

        ServiceScope = factory.Services.CreateScope();
        DbContext dbContext = ServiceScope.ServiceProvider.GetRequiredService<CbrDbContext>();
        _dbTransaction = dbContext.Database.BeginTransaction();
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