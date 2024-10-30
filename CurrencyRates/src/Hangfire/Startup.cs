using Cbr.Infrastructure;
using Hangfire.Helpers;
using Hangfire.Jobs;

namespace Hangfire;

internal static class Startup
{
    public static void AddServices(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddCbr(cfg);
        services.AddHttpClient();
        services.AddHangfire(cfg => cfg
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseInMemoryStorage());

        services.AddHangfireServer((provider, serverOptions) => 
        {
            serverOptions.Activator = new ServiceProviderAwareJobActivator(provider);
        });
    }

    public static void UseServices(this IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseHangfireDashboard("/hangfire");

        RecurringJob.AddOrUpdate<ImportCbrDayRatesJob>(
            "Import day rates from cbr api",
            s => s.RunAsync(new DateOnly(2023, 11, 12), CancellationToken.None),
            Cron.Hourly);
    }
}