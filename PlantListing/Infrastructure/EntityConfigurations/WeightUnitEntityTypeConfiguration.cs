using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantListing.Models;

namespace PlantListing.Infrastructure.EntityConfigurations
{
    public class WeightUnitEntityTypeConfiguration : IEntityTypeConfiguration<WeightUnit>
    {
        public void Configure(EntityTypeBuilder<WeightUnit> builder)
        {
            builder.ToTable("WeightUnit");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("weight_unit_hilo")
               .IsRequired();

            builder.Property(cb => cb.Unit)
                .IsRequired()
                .HasMaxLength(50);

            // To seed data
            builder.HasData(PlantListingContextSeed.GetPreconfiguredWeightUnits());
        }
    }
}
