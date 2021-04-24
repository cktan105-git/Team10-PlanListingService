using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantListing.Integrations
{
    public interface IProducerService
    {
        public bool TryGetProducerId(string userId, out long producerId);
    }
}
