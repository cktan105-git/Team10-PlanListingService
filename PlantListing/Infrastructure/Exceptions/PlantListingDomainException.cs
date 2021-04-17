using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantListing.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class PlantListingDomainException : Exception
    {
        public PlantListingDomainException()
        { }

        public PlantListingDomainException(string message)
            : base(message)
        { }

        public PlantListingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
