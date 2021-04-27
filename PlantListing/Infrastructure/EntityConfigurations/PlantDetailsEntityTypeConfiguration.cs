using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantListing.Models;

namespace PlantListing.Infrastructure.EntityConfigurations
{
    public class PlantDetailsEntityTypeConfiguration : IEntityTypeConfiguration<PlantDetails>
    {
        public void Configure(EntityTypeBuilder<PlantDetails> builder)
        {
            builder.ToTable("PlantDetails");

            builder.HasKey(ci => ci.PlantDetailsId);

            builder.Property(ci => ci.PlantDetailsId)
                .UseHiLo("plant_details_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(ci => ci.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(ci => ci.CategoryId)
                .IsRequired(true);

            builder.Property(ci => ci.Price)
                .IsRequired(true)
                .HasPrecision(18, 2);

            builder.Property(ci => ci.Weight)
                .IsRequired(true)
                .HasPrecision(18, 2);

            builder.Property(ci => ci.UnitId)
                .IsRequired(true);

            builder.Property(ci => ci.Stock)
                .IsRequired(true);

            builder.Property(ci => ci.ImageName)
                .IsRequired(false);

            builder.Property(ci => ci.UserId)
                .IsRequired(true)
                .HasMaxLength(100);

            // To seed data
            builder.HasData(PlantListingContextSeed.GetPreconfiguredPlantDetails());
        }
    }
}
