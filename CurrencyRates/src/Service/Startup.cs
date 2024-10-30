using Cbr.Infrastructure;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace Service;

public static class Startup
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddCbr(cfg);
        services.AddHttpClient();
        // TODO: попробовать добавить HttpClient в конфигурацию модуля Cbr
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSpaStaticFiles(cfg =>
        {
            cfg.RootPath = "cbr-frontend/dist";
        });
    }

    public static void UseServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseAuthorization();
        app.UseWhen(
            cxt => !cxt.Request.Path.StartsWithSegments("/api"),
            then =>
            {
                if (!app.Environment.IsDevelopment())
                {
                    then.UseSpaStaticFiles();
                }

                then.UseSpa(spa =>
                {
                    if (app.Environment.IsDevelopment())
                    {
                        spa.Options.SourcePath = "cbr-frontend";
                        spa.Options.DevServerPort = 5173;
                        spa.UseReactDevelopmentServer(npmScript: "start");
                    }
                });
            }
        );
        app.MapControllers();
    }

}
