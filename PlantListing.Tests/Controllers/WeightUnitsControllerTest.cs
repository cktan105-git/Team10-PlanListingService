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
    public class WeightUnitsControllerTest
    {
        private readonly DbContextOptions<PlantListingContext> _dbOptions;

        public WeightUnitsControllerTest()
        {
            _dbOptions = new DbContextOptionsBuilder<PlantListingContext>()
                .UseInMemoryDatabase(databaseName: "in-memory weight units")
                .Options;

            using (var dbContext = new PlantListingContext(_dbOptions))
            {
                if (!dbContext.WeightUnits.Any())
                {
                    dbContext.WeightUnits.AddRange(PlantListingContextSeed.GetPreconfiguredWeightUnits());
                }

                dbContext.SaveChanges();
            }
        }
      
        #region GetWeightUnits
        [Fact]
        public async Task Get_weight_units_success()
        {
            //Arrange
            var plantDetailsContext = new PlantListingContext(_dbOptions);
            var expectedCount = 3;

            //Act
            var weightUnitsController = new WeightUnitsController(plantDetailsContext);
            var actionResult = await weightUnitsController.GetWeightUnits();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<WeightUnit>>>(actionResult);
            Assert.Equal(expectedCount, actionResult.Value.Count());
        }
        #endregion
    }
}
