﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantListing.Extensions;
using PlantListing.Images;
using PlantListing.Infrastructure;
using PlantListing.Models;
using PlantListing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Authorization;

namespace PlantListing.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlantListingController : ControllerBase
    {
        private readonly PlantListingContext _context;
        private readonly IPlantImageService _plantImageService;

        public PlantListingController(PlantListingContext context, IPlantImageService plantImageService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _plantImageService = plantImageService ?? throw new ArgumentNullException(nameof(plantImageService));
        }

        // GET: api/v1/PlantListing/[?pageSize=5&pageIndex=10]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<PlantDetailsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<PlantDetailsViewModel>>> GetPlantListing([FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<PlantDetails>)_context.PlantDetails;

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<PlantDetailsViewModel>(pageIndex, pageSize, totalItems, MapToViewModels(itemsOnPage));
        }

        // GET: api/v1/PlantListing/{plantDetailsIds}
        [HttpGet]
        [Route("{plantDetailsIds}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<PlantDetailsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<PlantDetailsViewModel>>> GetPlantListingByPlantDetailsIds(string plantDetailsIds, [FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
            if (string.IsNullOrWhiteSpace(plantDetailsIds))
            {
                return BadRequest("Plant Details Ids must not be empty.");
            }

            var tupleIds = plantDetailsIds.Split('|').Select(id => (OK: long.TryParse(id, out long x), Value: x));
            if (!tupleIds.All(id => id.OK))
            {
                return BadRequest("Plant Details Ids must be pipe-separated list of numbers");
            }

            var idsToSelect = tupleIds.Select(id => id.Value);

            var root = (IQueryable<PlantDetails>)_context.PlantDetails;
            root = root.Where(d => idsToSelect.Contains(d.PlantDetailsId));

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<PlantDetailsViewModel>(pageIndex, pageSize, totalItems, MapToViewModels(itemsOnPage));
        }

        // GET: api/v1/PlantListing/MyPlantListing
        [HttpPost]
        [Route("MyPlantListing")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<PlantDetailsViewModel>), (int)HttpStatusCode.OK)]
        [Authorize(Policy = "Producers")]
        public async Task<ActionResult<PaginatedItemsViewModel<PlantDetailsViewModel>>> GetMyPlantListing([FromBody] GetMyPlantListingViewModel viewModel)
        {
            var root = (IQueryable<PlantDetails>)_context.PlantDetails;
            root = root.Where(d => d.UserId == viewModel.UserId);

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(viewModel.PageSize * viewModel.PageIndex)
                .Take(viewModel.PageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<PlantDetailsViewModel>(viewModel.PageIndex, viewModel.PageSize, totalItems, MapToViewModels(itemsOnPage));
        }

        // GET: api/v1/PlantListing/Category/{categoryId}
        [HttpGet]
        [Route("Category/{categoryId:int?}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<PlantDetailsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<PlantDetailsViewModel>>> GetPlantListingByCategoryId(int? categoryId, [FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<PlantDetails>)_context.PlantDetails;
            if (categoryId.HasValue)
            {
                root = root.Where(d => d.CategoryId == categoryId.Value);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<PlantDetailsViewModel>(pageIndex, pageSize, totalItems, MapToViewModels(itemsOnPage));
        }
       
        // GET: api/v1/PlantListing/Search/keyword[?categoryId=1&pageSize=5&pageIndex=10]
        [HttpGet]
        [Route("Search/{keyword:minlength(1)}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<PlantDetailsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<PlantDetailsViewModel>>> SearchPlantListing(string keyword, [FromQuery] int? categoryId = null, [FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { Message = $"Keyword should not be empty" });
            }

            var trimmedKeyword = keyword.Trim();

            var root = (IQueryable<PlantDetails>)_context.PlantDetails;
            root = root.Where(d => EF.Functions.Like(d.Name, $"%{trimmedKeyword}%") || EF.Functions.Like(d.Description, $"%{trimmedKeyword}%"));

            if (categoryId.HasValue)
            {
                root = root.Where(d => d.CategoryId == categoryId.Value);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<PlantDetailsViewModel>(pageIndex, pageSize, totalItems, MapToViewModels(itemsOnPage));
        }


        // GET: api/v1/PlantListing/PlantDetails/{plantDetailsId}
        [HttpGet]
        [Route("PlantDetails/{plantDetailsId:long?}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PlantDetailsViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PlantDetailsViewModel>> GetPlantDetails(long plantDetailsId)
        {
            if (plantDetailsId <= 0)
            {
                return BadRequest(new { Message = $"Invalid plant details id {plantDetailsId}" });
            }

            var plantDetails = await _context.PlantDetails.FindAsync(plantDetailsId);

            if (plantDetails == null)
            {
                return NotFound(new { Message = $"Plant details with id {plantDetailsId} not found." });
            }

            return MapToViewModel(plantDetails);
        }

        // PUT: api/v1/PlantListing/PlantDetails
        [HttpPut]
        [Route("PlantDetails")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Authorize(Policy = "Producers")]
        public async Task<IActionResult> UpdatePlantDetails([FromForm] CreateEditPlantDetailsViewModel plantDetailsViewModel)
        {
            var plantDetails = await _context.PlantDetails.FindAsync(plantDetailsViewModel.PlantDetailsId);
            if (plantDetails == null)
            {
                return NotFound(new { Message = $"Plant details with id {plantDetailsViewModel.PlantDetailsId} not found." });
            }

            if(plantDetails.UserId != plantDetailsViewModel.UserId)
            {
                return Unauthorized();
            }

            GetChangesFromViewModel(plantDetails, plantDetailsViewModel);
            if (!plantDetails.IsValid())
            {
                return BadRequest();
            }

            if ((plantDetailsViewModel.ImageFile != null && !plantDetailsViewModel.ImageFile.IsValidImage()))
            {
                return BadRequest(new { Message = $"Invalid image" });
            }

            try
            {
                if (plantDetailsViewModel.ImageFile != null)
                {
                    var currentImageName = plantDetails.ImageName;
                    var plantImageViewModel = await _plantImageService.ReplaceImageAsync(currentImageName, plantDetailsViewModel.ImageFile);
                    plantDetails.ImageName = plantImageViewModel.ImageName;
                }

                _context.Entry(plantDetails).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantDetailsExists(plantDetailsViewModel.PlantDetailsId))
                {
                    return NotFound(new { Message = $"Plant details with id {plantDetailsViewModel.PlantDetailsId} not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/v1/PlantListing/PlantDetails
        [HttpPost]
        [Route("PlantDetails")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(PlantDetailsViewModel), (int)HttpStatusCode.OK)]
        [Authorize(Policy = "Producers")]
        public async Task<ActionResult<PlantDetailsViewModel>> CreatePlantDetails([FromForm] CreateEditPlantDetailsViewModel plantDetailsViewModel)
        {
            var plantDetails = new PlantDetails();
            GetChangesFromViewModel(plantDetails, plantDetailsViewModel);
            plantDetails.UserId = plantDetailsViewModel.UserId;

            if (!plantDetails.IsValid())
            {
                return BadRequest();
            }

            if ((plantDetailsViewModel.ImageFile != null && !plantDetailsViewModel.ImageFile.IsValidImage()))
            {
                return BadRequest(new { Message = $"Invalid image" });
            }

            if (plantDetailsViewModel.ImageFile != null)
            {
                var plantImageViewModel = await _plantImageService.UploadImageAsync(plantDetailsViewModel.ImageFile);
                plantDetails.ImageName = plantImageViewModel.ImageName;
            }
         
            _context.PlantDetails.Add(plantDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlantDetails", new { id = plantDetails.PlantDetailsId }, MapToViewModel(plantDetails));
        }

        // DELETE: api/v1/PlantListing/PlantDetails
        [HttpDelete]
        [Route("PlantDetails")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Authorize(Policy = "Producers")]
        public async Task<IActionResult> DeletePlantDetails([FromBody] DeletePlantDetailsViewModel viewModel)
        {
            var plantDetails = await _context.PlantDetails.FindAsync(viewModel.PlantDetailsId);
            if (plantDetails == null)
            {
                return NotFound(new { Message = $"Plant details with id {viewModel.PlantDetailsId} not found." });
            }

            if (plantDetails.UserId != viewModel.UserId)
            {
                return Unauthorized();
            }

            _context.PlantDetails.Remove(plantDetails);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(plantDetails.ImageName))
            {
               _plantImageService.DeleteImageAsync(plantDetails.ImageName); // no need await for this
            }

            return NoContent();
        }

        private bool PlantDetailsExists(long id)
        {
            return _context.PlantDetails.Any(e => e.PlantDetailsId == id);
        }

        private PlantDetailsViewModel MapToViewModel(PlantDetails plantDetails)
        {
            return new PlantDetailsViewModel()
            {
                PlantDetailsId = plantDetails.PlantDetailsId,
                Name = plantDetails.Name,
                Description = plantDetails.Description,
                Category = _context.PlantCategories.Find(plantDetails.CategoryId)?.Category ?? string.Empty,
                Price = plantDetails.Price,
                Weight = plantDetails.Weight,
                Unit = _context.WeightUnits.Find(plantDetails.UnitId)?.Unit ?? string.Empty,
                Stock = plantDetails.Stock,
                ImageName = plantDetails.ImageName,
                ImageUri = _plantImageService.GetPlantImageUri(plantDetails.ImageName),
                UserId = plantDetails.UserId
            };
        }

        private List<PlantDetailsViewModel> MapToViewModels(List<PlantDetails> plantDetails)
        {
            return plantDetails.Select(d => MapToViewModel(d)).ToList();
        }

        private void GetChangesFromViewModel(PlantDetails plantDetails, CreateEditPlantDetailsViewModel viewModel)
        {
            var category = _context.PlantCategories.Where(c => EF.Functions.Like(c.Category, viewModel.Category ?? string.Empty)).FirstOrDefault();
            var weightUnit = _context.WeightUnits.Where(c => EF.Functions.Like(c.Unit, viewModel.Unit ?? string.Empty)).FirstOrDefault();

            plantDetails.Name = viewModel.Name;
            plantDetails.Description = viewModel.Description;
            plantDetails.CategoryId = category?.Id ?? -1;
            plantDetails.Price = viewModel.Price;
            plantDetails.Weight = viewModel.Weight;
            plantDetails.UnitId = weightUnit?.Id ?? -1;
            plantDetails.Stock = viewModel.Stock;
        }
    }
}
