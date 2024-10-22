using Cbr.Infrastructure.Database;
using Cbr.Infrastructure.Database.Repository;
using Cbr.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cbr.Infrastructure;

public static class CbrModuleSetup
{
    public static void AddCbr(this IServiceCollection services, IConfiguration cfg)
    {
        /*
        Repository
        */
        services.AddScoped<CurrencyRatesRepository>();

        /*
        Service
        */
        services.AddScoped<CurrencyRatesService>();

        services.AddDbContext<CbrDbContext>(o =>
        {
            o.UseNpgsql(cfg.GetConnectionString("Postgres"));
        });
    }
}
