using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbr.Infrastructure.Database.Configuration;

public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> builder)
    {
        builder.HasKey(r => new {r.SourceCurrency, r.TargetCurrency, r.Date });

        builder.Property(r => r.SourceCurrency)
            .HasColumnName("source_currency")
            .IsRequired();

        builder.Property(r => r.TargetCurrency)
            .HasColumnName("target_currency")
            .IsRequired();
            
            builder.Property(r => r.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(r => r.ExchangeRate)
            .HasPrecision(20, 4)
            .HasColumnName("exchange_rate")
            .IsRequired();

        builder.ToTable("currency_rate");
    }
}
