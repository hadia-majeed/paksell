using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using paksell;
using paksell.Db;
using PakSell.Models;
using System.Reflection.Metadata.Ecma335;

namespace PakSell.RestApis.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdvertisementCategoriesController : ControllerBase
    {
        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                List<AdvertisementCategory> entities = null;
                if (string.IsNullOrEmpty(Request.QueryString.Value))
                {
                    entities = new AdvertisementCategoryHandler().GetAdvertisementCategories();

               
                }
                return Ok(entities.ToModelList());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
