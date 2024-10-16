using Cbr.Application.UseCases.CurrencyRates.Save;
using CbrCurrencyRatesSaver;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.AddDependencies(builder.Configuration);

using IHost host = builder.Build();

new SaveCurrencyRatesFromFileHandler().Handle(new SaveCurrencyRatesFromFileCommand(args[0]));