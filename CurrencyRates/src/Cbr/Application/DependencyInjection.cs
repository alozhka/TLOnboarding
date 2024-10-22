using Cbr.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Cbr.Application;

public static class DependencyInjection
{
    public static void AddCbrApplication(this IServiceCollection services)
    {
        services.AddScoped<CurrencyRatesService>();
    }
}
