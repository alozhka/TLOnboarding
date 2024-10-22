using Cbr.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbr.Infrastructure.Database.Configuration;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(c => c.Code);

        builder.Property(c => c.Code)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasColumnName("code")
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(64)
            .HasColumnName("name")
            .IsRequired();

        builder.ToTable("currency");
    }
}
