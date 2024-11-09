using Cbr.Infrastructure;
using Hangfire;
using Hangfire.PostgreSql;
using HangfireServer.Configuration;

namespace HangfireServer;

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
                .UseRecommendedSerializerSettings()
                .UseInMemoryStorage()
                /*.UsePostgreSqlStorage(bootstrapperOptions =>
                    bootstrapperOptions.UseNpgsqlConnection(cfg.GetConnectionString("Postgres")))*/;
        });

        services.AddHangfireServer((provider, serverOptions) =>
        {
            serverOptions.Activator = new ServiceProviderAwareJobActivator(provider);
        });
        services.AddHostedService<RecurringJobInstallService>();
    }

    public static void UseServices(this IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseHangfireDashboard();
    }
}