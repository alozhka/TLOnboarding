using Cbr.Domain.Entity;
using Cbr.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbr.Infrastructure.Database.Configuration;

public class CurrencyRatesConfiguration : IEntityTypeConfiguration<CurrencyRates>
{
    public void Configure(EntityTypeBuilder<CurrencyRates> builder)
    {
        builder.HasKey(cr => cr.Date)
            .HasName("date");

        builder.HasMany(cr => cr.Rates)
            .WithMany()
            .UsingEntity("currency_rate_currency_rates");

        builder.ToTable("currency_rates");
    }
}
