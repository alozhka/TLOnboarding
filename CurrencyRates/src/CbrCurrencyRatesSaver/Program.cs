using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

using IHost host = builder.Build();

await host.RunAsync();