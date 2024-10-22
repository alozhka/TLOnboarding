using Cbr.Application.Service;
using CbrCurrencyRatesSaver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.AddDependencies(builder.Configuration);

using IHost host = builder.Build();

CurrencyRatesService currencyRatesService = host.Services.GetRequiredService<CurrencyRatesService>();

currencyRatesService.SaveDayRatesFromFile(args[0]);