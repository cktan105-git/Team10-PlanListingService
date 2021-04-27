using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantListing.ViewModels
{
    public class GetMyPlantListingViewModel
    {
        public string UserId { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 0;
    }
}
