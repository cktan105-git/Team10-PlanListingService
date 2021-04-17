using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlantListing.ViewModels;

namespace PlantListing.Images
{
    public interface IPlantImageService
    {
        public Task<PlantImageViewModel> UploadImageAsync(IFormFile file);
        public Task<PlantImageViewModel> ReplaceImageAsync(string oldFileName, IFormFile file);
        public Task<bool> DeleteImageAsync(string fileName);
        public string GetPlantImageUri(string fileName);
    }
}
