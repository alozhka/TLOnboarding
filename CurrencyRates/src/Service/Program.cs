using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSpaStaticFiles(cfg => 
{
    cfg.RootPath = "cbr-frontend/build";
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
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


app.Run();
