using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace SpecsLibrary.Fixtures;

public class CustomWebApplicationFactory<T>(Action<IServiceCollection>? configureServices = null)
    : WebApplicationFactory<T> where T : class
{
    private const string TestEnvironment = "Test";
    private const string TestConfigName = "appsettings.tests.json";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(TestEnvironment);
        builder.ConfigureHostConfiguration(configurationBuilder =>
        {
            configurationBuilder.AddJsonFile(
                new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory),
                TestConfigName,
                optional: false,
                reloadOnChange: false);
        });
        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        if (configureServices != null) builder.ConfigureServices(configureServices);
        base.ConfigureWebHost(builder);
    }
}