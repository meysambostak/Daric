



using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.Entities;
using DaricTemplate.Core.Domain.BaseInfo.CountryLocations.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaricTemplate.Infra.Data.Commands.BaseInfo.CountryLocations.Configs;

public class CountryLocationConfiguration : IEntityTypeConfiguration<CountryLocation>
{
    public void Configure(EntityTypeBuilder<CountryLocation> builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

      //  builder.ToTable ("CountryLocations", typeof(CountryLocation).GetSchema());
        builder.Property(e => e.Code)
          .HasConversion(c => c.Value, c => Code.FromString(c))
          .HasMaxLength(10).IsRequired();
        builder.Property(e => e.Abbreviation)
          .HasConversion(c => c.Value, c => Abbreviation.FromString(c))
          .HasMaxLength(30);
        builder.Property(e => e.Title)
         .HasMaxLength(255).IsRequired(); ;
        builder.Property(e => e.AlternativeTitle)
         .HasMaxLength(255);
        builder.Property(e => e.LocationType)
         .IsRequired(); 


        builder
        .HasOne(u => u.ParentCountryLocation)
        .WithMany(r => r.CountryLocations)
        .HasForeignKey(parentCountry => parentCountry.ParentCountryLocationId)
        .HasPrincipalKey(p => p.BusinessId)
        .OnDelete(DeleteBehavior.Restrict);



    }
}

