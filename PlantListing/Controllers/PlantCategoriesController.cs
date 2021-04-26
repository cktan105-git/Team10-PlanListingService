using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PlantListing.Infrastructure;
using PlantListing.Models;
using Microsoft.AspNetCore.Authorization;

namespace PlantListing.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlantCategoriesController : ControllerBase
    {
        private readonly PlantListingContext _context;

        public PlantCategoriesController(PlantListingContext context)
        {
            _context = context;
        }

        // GET: api/v1/PlantCategories
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlantCategory>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PlantCategory>>> GetPlantCategories()
        {
            return await _context.PlantCategories.ToListAsync();
        }
    }
}
