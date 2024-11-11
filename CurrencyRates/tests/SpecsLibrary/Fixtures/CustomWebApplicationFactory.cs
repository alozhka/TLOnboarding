using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace SpecsLibrary.Fixtures;

public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    private const string TestEnvironment = "Test";
    private const string TestConfigName = "appsettings.Test.json";

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
}