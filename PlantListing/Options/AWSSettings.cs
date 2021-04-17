using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantListing.Options
{
    public class AWSSettings
    {
        public const string AWS = "AWS";

        public string ImageS3BucketName { get; set; }
        public string ImageCloudFrontDomainName { get; set; }
    }
}
