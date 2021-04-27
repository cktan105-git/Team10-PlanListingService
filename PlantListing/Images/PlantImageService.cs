using Amazon.S3;
using Amazon.S3.Model;
using PlantListing.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PlantListing.Extensions;
using PlantListing.Infrastructure.Exceptions;
using PlantListing.ViewModels;

namespace PlantListing.Images
{
    public class PlantImageService : IPlantImageService
    {
        private readonly IOptions<AWSSettings> _options;
        private readonly IAmazonS3 _s3Client;
        private readonly ILogger _logger;

        public PlantImageService(IOptions<AWSSettings> options, IAmazonS3 s3Client, ILogger<PlantImageService> logger)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PlantImageViewModel> UploadImageAsync(IFormFile file)
        {
            // get the file and convert it to the byte[]
            byte[] fileBytes = new Byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

            // create unique file name for prevent the mess
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            return await UploadImageAsync(fileName, file.ContentType, fileBytes); ;
        }

        public async Task<PlantImageViewModel> ReplaceImageAsync(string oldFileName, IFormFile file)
        {
            if(!string.IsNullOrEmpty(oldFileName))
            {
                DeleteImageAsync(oldFileName); // no need await for this
            }

            return await UploadImageAsync(file);
        }

        public async Task<bool> DeleteImageAsync(string fileName)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = _options.Value.ImageS3BucketName,
                    Key = fileName
                };

                var response = await _s3Client.DeleteObjectAsync(request);

                if(response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    string message = $"Fail to delete image: {fileName}";

                    var json = new
                    {
                        Message = message,
                        Response = JsonConvert.SerializeObject(response)
                    };

                    _logger.LogInformation(JsonConvert.SerializeObject(json)); ;
                    //throw new PlantListingDomainException(message); // dont throw exception if fail to delete image from S3

                    return false;
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                _logger.LogError(amazonS3Exception.ToString());

                // dont throw exception if fail to delete image from S3
                //if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                //{
                //    throw new Exception("Check the provided AWS Credentials.");
                //}
                //else
                //{
                //    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                //}

                return false;
            }
        }

        private async Task<PlantImageViewModel> UploadImageAsync(string fileName, string contentType, byte[] fileBytes)
        {
            try
            {
                PutObjectResponse response = null;

                using (var stream = new MemoryStream(fileBytes))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = _options.Value.ImageS3BucketName,
                        Key = fileName,
                        InputStream = stream,
                        ContentType = contentType,
                        //CannedACL = S3CannedACL.PublicRead // enable public read access
                    };

                    response = await _s3Client.PutObjectAsync(request);
                };

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new PlantImageViewModel
                    {
                        ImageName = fileName,
                    };
                }
                else
                {
                    string message = $"Fail to upload image: {fileName}";

                    var json = new
                    {
                        Message = message,
                        Response = JsonConvert.SerializeObject(response)
                    };

                    _logger.LogInformation(JsonConvert.SerializeObject(json)); ;
                    throw new PlantListingDomainException(message);
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                _logger.LogError(amazonS3Exception.ToString());

                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
        }

        public string GetPlantImageUri(string fileName)
        {
            if(string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            return $"https://{_options.Value.ImageCloudFrontDomainName}/{fileName}";
        }
    }
}
