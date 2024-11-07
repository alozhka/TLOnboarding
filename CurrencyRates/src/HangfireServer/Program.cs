using HangfireServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);
var app = builder.Build();

app.UseServices();


app.Run();

public class HangfireProgram;