using CbrCurrencyRatesSaver;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.AddDependencies(builder.Configuration);

using IHost host = builder.Build();

await host.RunAsync();