using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantListing.Infrastructure.EntityConfigurations;
using PlantListing.Models;

namespace PlantListing.Infrastructure
{
    public class PlantListingContext : DbContext
    {
        public PlantListingContext(DbContextOptions<PlantListingContext> options) : base(options)
        {
        }

        public DbSet<PlantDetails> PlantDetails { get; set; }
        public DbSet<PlantCategory> PlantCategories { get; set; }
        public DbSet<WeightUnit> WeightUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PlantCategoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new WeightUnitEntityTypeConfiguration());
            builder.ApplyConfiguration(new PlantDetailsEntityTypeConfiguration());
        }
    }
}
