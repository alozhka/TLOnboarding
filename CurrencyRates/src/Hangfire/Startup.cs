namespace Hangfire;

internal static class Startup
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddHangfire(cfg => cfg
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseInMemoryStorage());
        
        services.AddHangfireServer();
    }

    public static void UseServices(this IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseHangfireDashboard("/hangfire");
        
        RecurringJob.AddOrUpdate(
            "Write current date in console",
            () => WriteCurrentDateInConsole(),
            Cron.Minutely);
    }

    public static void WriteCurrentDateInConsole()
    {
        DateTime currentTime = DateTime.Now;
        Console.WriteLine("Running simple task at " + currentTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}