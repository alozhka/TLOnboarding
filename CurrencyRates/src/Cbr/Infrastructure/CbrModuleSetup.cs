using System.Text;
using Cbr.Application;
using Cbr.Application.Abstractions;
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
        services.AddCbrApplication();
        
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        /*
        Repository
        */
        services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();

        /*
        Service
        */
        services.AddTransient<ICbrXmlParser, CbrXmlParser>();
        services.AddTransient<ICbrApiService, CbrApiService>();

        services.AddDbContext<CbrDbContext>(o =>
        {
            o.UseNpgsql(cfg.GetConnectionString("Postgres"));
            o.EnableSensitiveDataLogging();
        });
    }
}
