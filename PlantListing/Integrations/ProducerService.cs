using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlantListing.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlantListing.Integrations
{
    public class ProducerService : IProducerService
    {
        private readonly ProducerServiceClient _client;

        public ProducerService(ProducerServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            this._client = client;
        }

        public bool TryGetProducerId(string userId, out long producerId)
        {
            // TODO: Integrate with user service
            producerId = 1;
            return true;
        }
    }
}
