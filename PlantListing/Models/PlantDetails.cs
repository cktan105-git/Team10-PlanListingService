using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantListing.Models
{
    public class PlantDetails
    {
        public long PlantDetailsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public decimal Weight { get; set; }

        public int UnitId { get; set; }

        public int Stock { get; set; }

        public string ImageName { get; set; }

        public string UserId { get; set; }

        public bool IsValid()
        {
            if(string.IsNullOrWhiteSpace(Name) || Name.Length > 100 || (!string.IsNullOrEmpty(Description) && Description.Length > 500) || CategoryId <= 0 || Price < 0.00m || Weight <= 0.00m || UnitId <= 0 || Stock < 0 || string.IsNullOrEmpty(UserId) )
            {
                return false;
            }

            return true;
        }
    }
}
