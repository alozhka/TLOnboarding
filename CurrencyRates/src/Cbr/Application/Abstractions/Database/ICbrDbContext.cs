using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cbr.Application.Abstractions.Database;

public interface ICbrDbContext
{
    public DbSet<CurrencyRate> CurrencyRate { get; set; }
    public DbSet<CurrencyRates> CurrencyRates { get; set; }
}
