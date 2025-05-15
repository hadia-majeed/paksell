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

                // Check for standard claim types first
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // If standard claim not found, check for custom claim
                if (string.IsNullOrEmpty(userId))
                {
                    userId = User.FindFirstValue("UserId"); // Try common variations
                }

                if (string.IsNullOrEmpty(userId))
                {
                    userId = User.FindFirstValue("sub"); // JWT standard subject claim
                }

                // As a last resort, try to find any claim that might contain a user ID
                if (string.IsNullOrEmpty(userId))
                {
                    var userIdClaim = User.Claims.FirstOrDefault(c =>
                        c.Type.Contains("id", StringComparison.OrdinalIgnoreCase) ||
                        c.Type.Contains("user", StringComparison.OrdinalIgnoreCase));

                    if (userIdClaim != null)
                    {
                        userId = userIdClaim.Value;
                    }
                }

                // If still no user ID found, return unauthorized
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User ID not found in token. Please verify token contains the required claims.");
                }

                // Try to parse the user ID as an integer
                if (!int.TryParse(userId, out int parsedUserId))
                {
                    return BadRequest($"Invalid user ID format: {userId}");
                }
                var bindingmodel = model.ToModel();
                // Add the advertisement
                Advertisement? entity = new AdvertisementHandler().AddAdvertisement(bindingmodel.ToEntity());

                if (entity == null)
                {
                    return StatusCode(500, "Failed to create advertisement");
                }

                return Created($"{Request.Path}/{entity.Id}", entity.ToModel());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the advertisement: {ex.Message}");
            }
        }
    }
}