using Cbr.Domain.Entity;
using Cbr.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database;

public class CurrencyRatesDdContext : DbContext
{
    public DbSet<CurrencyRate> CurrencyRate { get; set; }
    public DbSet<CurrencyRates> CurrencyRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=currency-rates;Username=currencyratesapi;Password=em4xooNu");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyRatesConfiguration());
    }
}
