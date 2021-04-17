using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantListing.Models;

namespace PlantListing.Infrastructure
{
    public class PlantListingContextSeed
    {
        public async Task SeedAsync(PlantListingContext context, IWebHostEnvironment env, ILogger<PlantListingContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(PlantListingContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.PlantCategories.Any())
                {
                    await context.PlantCategories.AddRangeAsync(GetPreconfiguredPlantCategories());
                    await context.SaveChangesAsync();
                }

                if (!context.WeightUnits.Any())
                {
                    await context.WeightUnits.AddRangeAsync(GetPreconfiguredWeightUnits());
                    await context.SaveChangesAsync();
                }

                if (!context.PlantDetails.Any())
                {
                    await context.PlantDetails.AddRangeAsync(GetPreconfiguredPlantDetails());
                    await context.SaveChangesAsync();
                }
            });
        }

        public static IEnumerable<PlantCategory> GetPreconfiguredPlantCategories()
        {
            return new List<PlantCategory>()
            {
                new PlantCategory() { Id = 1, Category = "Vegetable" },
                new PlantCategory() { Id = 2, Category = "Fruit" },
                new PlantCategory() { Id = 3, Category = "Flower" },
                new PlantCategory() { Id = 4, Category = "Herb" },
                new PlantCategory() { Id = 5, Category = "Spice" },
            };
        }

        public static IEnumerable<WeightUnit> GetPreconfiguredWeightUnits()
        {
            return new List<WeightUnit>()
            {
                new WeightUnit() { Id = 1, Unit = "kg" },
                new WeightUnit() { Id = 2, Unit = "g" },
                new WeightUnit() { Id = 3, Unit = "bundle" },
            };
        }

        public static IEnumerable<PlantDetails> GetPreconfiguredPlantDetails()
        {
            return new List<PlantDetails>()
            {
                new PlantDetails() { PlantDetailsId = 1, Name = "Broccoli", Description = "Green vegetable", CategoryId = 1, Price = 2.00m, Weight = 1.0m, UnitId = 1, Stock = 50, ProducerId = 1, ImageName = "898ed47a-c779-4d07-bcf5-d241adc55f89_Broccoli.jpg" },
                new PlantDetails() { PlantDetailsId = 2, Name = "Tomato", Description = "Red color sour fruit", CategoryId = 2, Price = 1.00m, Weight = 0.5m, UnitId = 1, Stock = 50, ProducerId = 1, ImageName = "ddbd68bd-da6d-4ba2-a70f-5a32813ec261_Tomato.jpg" },
                new PlantDetails() { PlantDetailsId = 3, Name = "Japanese Cucumber", Description = "Green color fruit", CategoryId = 2, Price = 1.00m, Weight = 500.0m, UnitId = 2, Stock = 50, ProducerId = 2, ImageName = "af25243b-9f08-4c1f-ad8f-d031695154d5_Japanese Cucumber.jpg" },
                new PlantDetails() { PlantDetailsId = 4, Name = "Sunflower", Description = "Flower chasing the sun", CategoryId = 3, Price = 50.00m, Weight = 1, UnitId = 3, Stock = 10, ProducerId = 3, ImageName = "df9b2a47-3975-474d-82b6-5f6bcd88f743_Sunflower.jpg" },
                new PlantDetails() { PlantDetailsId = 5, Name = "Garlic", Description = "Home grown fresh garlic", CategoryId = 5, Price = 0.50m, Weight = 100.0m, UnitId = 2, Stock = 50, ProducerId = 4, ImageName = "a624fc48-0349-4e28-985e-d5aa4c859c0c_Garlic.jpg" },
                new PlantDetails() { PlantDetailsId = 6, Name = "Spring Onion", Description = "Add flavor to your dish", CategoryId = 5, Price = 0.50m, Weight = 100.0m, UnitId = 2, Stock = 50, ProducerId = 4, ImageName = "1c1b6571-cbbe-4f45-a5ed-01704a8885b8_Spring Onion.jpg" },
                new PlantDetails() { PlantDetailsId = 7, Name = "Red Chilli", Description = "Red Chilli", CategoryId = 5, Price = 1.00m, Weight = 100.0m, UnitId = 2, Stock = 100, ProducerId = 4, ImageName = "1a048a12-4815-4ce1-9df3-02088c3f82c9_Red Chilli.jpg" },
                new PlantDetails() { PlantDetailsId = 8, Name = "Green Chilli", Description = "Green Chilli", CategoryId = 5, Price = 1.00m, Weight = 100.0m, UnitId = 2, Stock = 100, ProducerId = 4, ImageName = "e095626b-d72f-4fc7-ac7b-0fcc6393ae00_Green Chilli.jpg" }
            };
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<PlantListingContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
