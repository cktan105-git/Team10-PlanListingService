using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantListing.ViewModels
{
    public class CreateUpdatePlantDetailsViewModel
    {
        public long PlantDetailsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public string Unit { get; set; }
        public int Stock { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
