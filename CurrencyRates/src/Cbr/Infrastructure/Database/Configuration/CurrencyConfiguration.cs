using System;
using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbr.Infrastructure.Database.Configuration;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
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

        builder.ToTable("currency");
    }
}
