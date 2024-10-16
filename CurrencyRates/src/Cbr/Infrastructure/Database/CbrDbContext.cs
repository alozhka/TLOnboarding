using Cbr.Domain.Entity;
using Cbr.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Infrastructure.Database;

public class CbrDbContext : DbContext
{
    public DbSet<CurrencyRate> CurrencyRate { get; set; }
    public DbSet<CurrencyRates> CurrencyRates { get; set; }

    public CbrDbContext(DbContextOptions<CbrDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Cbr");

        modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyRatesConfiguration());
    }
}
