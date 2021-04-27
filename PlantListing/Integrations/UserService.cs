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
    public class UserService : IUserService
    {
        public string GetUserId()
        {
            // TODO: Integrate with user service
            return "wpkeoh";
        }
    }
}
