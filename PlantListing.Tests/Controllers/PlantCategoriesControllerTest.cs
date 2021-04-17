using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PlantListing.Controllers;
using PlantListing.Infrastructure;
using PlantListing.Models;
using PlantListing.ViewModels;
using Xunit;

namespace PlantListing.Test
{
    public class PlantCategoriesControllerTest
    {
        private readonly DbContextOptions<PlantListingContext> _dbOptions;

        public PlantCategoriesControllerTest()
        {
            _dbOptions = new DbContextOptionsBuilder<PlantListingContext>()
                .UseInMemoryDatabase(databaseName: "in-memory plant categories")
                .Options;

            using (var dbContext = new PlantListingContext(_dbOptions))
            {
                if (!dbContext.PlantCategories.Any())
                {
                    dbContext.PlantCategories.AddRange(PlantListingContextSeed.GetPreconfiguredPlantCategories());
                }

                dbContext.SaveChanges();
            }
        }
      
        #region GetPlantCategories
        [Fact]
        public async Task Get_plant_categories_success()
        {
            //Arrange
            var plantDetailsContext = new PlantListingContext(_dbOptions);
            var expectedCount = 5;

            //Act
            var plantCategoriesController = new PlantCategoriesController(plantDetailsContext);
            var actionResult = await plantCategoriesController.GetPlantCategories();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<PlantCategory>>>(actionResult);
            Assert.Equal(expectedCount, actionResult.Value.Count());
        }
        #endregion
    }
}
