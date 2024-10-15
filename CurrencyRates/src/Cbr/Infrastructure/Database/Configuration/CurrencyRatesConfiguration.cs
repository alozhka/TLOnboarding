using Cbr.Domain.Entity;
using Cbr.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cbr.Infrastructure.Database.Configuration;

public class CurrencyRatesConfiguration : IEntityTypeConfiguration<CurrencyRates>
{
    public void Configure(EntityTypeBuilder<CurrencyRates> builder)
    {
        builder.HasKey(cr => cr.Date);

        builder.HasMany(cr => cr.Currencies)
        .WithMany()
        .UsingEntity<CurrencyRateDate>("CurrencyRate_Date", j =>
            {
                j.Property(crd => crd.VunitRate).IsRequired();
            });
    }
}
