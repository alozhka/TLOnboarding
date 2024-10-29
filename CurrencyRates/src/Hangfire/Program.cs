using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();
var app = builder.Build();

app.UseServices();

    
app.Run();
