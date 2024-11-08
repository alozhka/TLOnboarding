using Cbr.Infrastructure;
using Cbr.Infrastructure.Database;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HangfireServer.Specs.Fixtures;

public class HangfireServerFixture : IDisposable
{
    private readonly IDbContextTransaction _dbTransaction;
    public readonly HttpClient HttpClient;

    public HangfireServerFixture()
    {
        WebApplicationFactory<Program> factory = new();
        IConfiguration cfg = factory.Services.GetRequiredService<IConfiguration>();
        factory.WithWebHostBuilder(b =>
        {
            b.UseSolutionRelativeContentRoot("src/HangfireServer");
            b.UseEnvironment("Development");
            b.ConfigureServices(services =>
            {
                // Для тестов в Hangfire из-за проблем с откатом используется база в оперативе
                GlobalConfiguration.Configuration.UseInMemoryStorage();
                services.AddCbr(cfg);


                services.AddLogging(loggingBuilder =>
                    loggingBuilder.AddConsole().AddFilter(level => level >= LogLevel.Warning));
            });
        });

        DbContext dbContext = factory.Services.GetRequiredService<CbrDbContext>();
        _dbTransaction = dbContext.Database.BeginTransaction();
        HttpClient = factory.CreateClient();
    }

    public void Dispose()
    {
        _dbTransaction.Rollback();
        _dbTransaction.Dispose();

        GC.SuppressFinalize(this);
    }

    ~HangfireServerFixture()
    {
        Dispose();
    }
}