using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbr.Infrastructure.Database.Configuration;

public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> builder)
    {
        builder.HasKey(r => new {r.SourceCurrencyCode, r.TargetCurrencyCode, r.Date });
            
            builder.Property(r => r.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(r => r.ExchangeRate)
            .HasPrecision(20, 4)
            .HasColumnName("exchange_rate")
            .IsRequired();

        builder.HasOne(r => r.SourceCurrency)
            .WithMany()
            .HasForeignKey(cr => cr.SourceCurrencyCode);

        builder.HasOne(r => r.TargetCurrency)
            .WithMany()
            .HasForeignKey(cr => cr.TargetCurrencyCode);

        builder.ToTable("currency_rate");
    }
}
