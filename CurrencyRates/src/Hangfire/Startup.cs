using Cbr.Infrastructure;
using Hangfire.Helpers;
using Hangfire.Jobs;
using Hangfire.PostgreSql;

namespace Hangfire;

internal static class Startup
{
    public static void AddServices(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddCbr(cfg);
        services.AddHangfire(globalConfiguration =>
        {
            globalConfiguration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
            if (cfg["Applications:EnableIntegrationTesting"] == "true")
            {
                globalConfiguration.UseInMemoryStorage();
            }
            else
            {
                globalConfiguration.UsePostgreSqlStorage(bootstrapperOptions =>
                    bootstrapperOptions.UseNpgsqlConnection(cfg.GetConnectionString("Postgres")));
            }
        });


        services.AddHangfireServer((provider, serverOptions) =>
        {
            serverOptions.Activator = new ServiceProviderAwareJobActivator(provider);
        });
    }

    public static void UseServices(this IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseHangfireDashboard();

        RecurringJob.AddOrUpdate<ImportCbrDayRatesJob>(
            "Import day rates from cbr api",
            s => s.RunAsync(CancellationToken.None),
            Cron.Hourly);
    }
}