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
            .UsingEntity<CurrencyRate>(j =>
            {
                j.ToTable("currency_rate");
                j.Property(r => r.ExchangeRate)
                    .HasPrecision(20, 4)
                    .HasColumnName("exchange_rate")
                    .IsRequired();
            });

        builder.ToTable("currency_rates");
    }
}
