using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantListing.Models;

namespace PlantListing.Infrastructure.EntityConfigurations
{
    public class PlantCategoryEntityTypeConfiguration : IEntityTypeConfiguration<PlantCategory>
    {
        public void Configure(EntityTypeBuilder<PlantCategory> builder)
        {
            builder.ToTable("PlantCategory");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .UseHiLo("plant_category_hilo")
               .IsRequired();

            builder.Property(cb => cb.Category)
                .IsRequired()
                .HasMaxLength(100);

            // To seed data
            builder.HasData(PlantListingContextSeed.GetPreconfiguredPlantCategories());
        }
    }
}
