using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbr.Infrastructure.Database.Configuration;

public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> builder)
    {
        builder.HasKey(r => r.CurrencyCode);

        builder.Property(r => r.CurrencyCode)
        .HasMaxLength(3)
        .HasColumnName("currency_code")
        .IsFixedLength()
        .IsRequired();

        builder.Property(r => r.CurrencyName)
            .HasMaxLength(64)
            .HasColumnName("currency_name")
            .IsRequired();

        builder.Property(r => r.ExchangeRate)
            .HasPrecision(20, 4)
            .HasColumnName("exchange_rate")
            .IsRequired();

        builder.ToTable("currency_rate");
    }
}
