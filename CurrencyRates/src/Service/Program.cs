using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSpaStaticFiles(cfg => 
{
    cfg.RootPath = "cbr-frontend/build";
});


var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
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
                spa.UseReactDevelopmentServer(npmScript: "dev");
            }
        });
    }
);
app.MapControllers();


app.Run();
