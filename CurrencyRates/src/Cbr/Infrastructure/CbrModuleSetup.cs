using Cbr.Application.Abstractions.Database;
using Cbr.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cbr.Infrastructure;

public static class CbrModuleSetup
{
    public static void AddCbr(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<ICbrDbContext, CbrDbContext>(o =>
        {
            o.UseNpgsql(cfg.GetConnectionString("Postgres"));
        });
    }
}
