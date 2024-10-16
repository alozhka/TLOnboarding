using Cbr.Application.UseCases.CurrencyRates.Save;
using CbrCurrencyRatesSaver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.AddDependencies(builder.Configuration);

using IHost host = builder.Build();

SaveCurrencyRatesFromFileHandler saveCurrencyRates = host.Services.GetRequiredService<SaveCurrencyRatesFromFileHandler>();

saveCurrencyRates.Handle(new SaveCurrencyRatesFromFileCommand(args[0]));