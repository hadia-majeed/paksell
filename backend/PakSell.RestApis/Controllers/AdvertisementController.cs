using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using paksell;
using paksell.Db;
using PakSell.Models;
using System.Security.Claims;
using System.Text.Json;

namespace PakSell.RestApis.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Advertisement> entities = null;
                if (string.IsNullOrEmpty(Request.QueryString.Value))
                {
                    entities = new AdvertisementHandler().GetAdvertisements();
                }
                return Ok(entities?.ToModelList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving advertisements");
            }
        }

        [HttpGet("GetByCategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            try
            {
                List<Advertisement> entities = null;
                if (!string.IsNullOrEmpty(Request.QueryString.Value))
                {
                    entities = new AdvertisementHandler().GetAdvertisementsByCategory(categoryId);
                }
                return Ok(entities?.ToModelList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving advertisements by category");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Advertisement? entity = new AdvertisementHandler().GetAdvertisement(id);
                if (entity == null)
                {
                    return NotFound($"Advertisement with ID {id} not found");
                }
                return Ok(entity.ToModel());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the advertisement");
            }
        }

        [HttpPost]
        [Authorize]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] AdvertisementBindingModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Advertisement model cannot be null");
                }

                // Get the user ID from the claims
                var userIdClaim = User.FindFirst("User_Id");
                if (userIdClaim == null)
                {
                    return Unauthorized("User ID not found in token");
                }

                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    return BadRequest($"Invalid user ID format: {userIdClaim.Value}");
                }

                // Create a new advertisement entity
                var advertisement = new Advertisement
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    PostedById = userId,
                    startsOn = DateOnly.FromDateTime(model.StartsOn),
                    endsOn = DateOnly.FromDateTime(model.EndsOn),

                    // Create features collection
                    AdvertisementFeatures = model.AdvertisementFeatures?.Select(feature => new AdvertisementFeature
                    {
                        Name = feature,
                        value = feature  // You may need to adjust this based on your requirements
                    }).ToList() ?? new List<AdvertisementFeature>(),

                    // Create images collection
                    AdvertisementImages = model.AdvertisementImages?.Select((imagePath, index) => new AdvertisementImage
                    {
                        ImagePath = imagePath,
                        Rank = index  // Use the index as the rank to maintain order
                    }).ToList() ?? new List<AdvertisementImage>()
                };

                // Set category reference
                if (model.CategoryId > 0)
                {
                    advertisement.Category = new AdvertisementCategory { Id = model.CategoryId };
                }

                if (model.CityAreaId > 0)
                {
                    advertisement.CityArea = new CityArea { Id = model.CityAreaId };
                }

                // Add the advertisement with all its related entities
                var addedEntity = new AdvertisementHandler().AddAdvertisement(advertisement);

                if (addedEntity == null)
                {
                    return StatusCode(500, "Failed to create advertisement");
                }

                return Created($"{Request.Path}/{addedEntity.Id}", addedEntity.ToModel());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the advertisement: {ex.Message}");
            }
        }
    }
}