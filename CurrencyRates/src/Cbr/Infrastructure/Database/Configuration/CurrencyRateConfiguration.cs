using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbr.Infrastructure.Database.Configuration;

public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> builder)
    {
        builder.HasKey(r => r.CharCode);

        builder.Property(r => r.CharCode)
        .HasMaxLength(3)
        .HasColumnName("char_code")
        .IsFixedLength()
        .IsRequired();

        builder.Property(r => r.Name)
            .HasMaxLength(64)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(r => r.VUnitRate)
            .HasPrecision(20, 4)
            .HasColumnName("v_unit_rate")
            .IsRequired();

        builder.ToTable("currency_rate");
    }
}
