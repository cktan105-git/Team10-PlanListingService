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

namespace PlantListing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WeightUnitsController : ControllerBase
    {
        private readonly PlantListingContext _context;

        public WeightUnitsController(PlantListingContext context)
        {
            _context = context;
        }

        // GET: api/v1/WeightUnits
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeightUnit>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<WeightUnit>>> GetWeightUnits()
        {
            return await _context.WeightUnits.ToListAsync();
        }
    }
}
