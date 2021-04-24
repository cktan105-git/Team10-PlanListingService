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
    public class ProducerServiceClient
    {
        private readonly HttpClient _client;
        private readonly IOptions<ProducerServiceSettings> _options;
        private readonly ILogger _logger;

        public ProducerServiceClient(HttpClient client, IOptions<ProducerServiceSettings> options, ILogger<ProducerServiceClient> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            //client.BaseAddress = new Uri(_options.Value.BaseAddress);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this._client = client;
        }
    }
}
