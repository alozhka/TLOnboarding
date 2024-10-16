using Cbr.Domain.Entity;
using Cbr.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database;

public class CbrDbContext(DbContextOptions<CbrDbContext> options) : DbContext(options)
{
    public DbSet<CurrencyRate> CurrencyRate { get; set; }
    public DbSet<CurrencyRates> CurrencyRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.LogTo(Console.WriteLine);
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Cbr");

        modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyRatesConfiguration());
    }
}
