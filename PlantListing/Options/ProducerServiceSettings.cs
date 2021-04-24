using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantListing.Options
{
    public class ProducerServiceSettings
    {
        public const string ProducerService = "ProducerService";

        public string BaseAddress { get; set; }
        public string Environment { get; set; }
        public string Version { get; set; }
    }
}
