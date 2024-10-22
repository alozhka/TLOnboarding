using Cbr.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CbrCurrencyRatesSaver;

public static class Startup
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddCbr(cfg);
    }
}
