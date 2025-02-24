using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using paksell;
using paksell.Db;
using PakSell.Models;

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
                //ModelHelper.
                List<Advertisement> entities = null;
                if (string.IsNullOrEmpty(Request.QueryString.Value))
                {
                    entities = new AdvertisementHandler().GetAdvertisements();
                }
                return Ok(entities?.ToModelList());
            }

            catch (Exception ex) 
            {
                return StatusCode(500); 
            }
        }

        [HttpGet("GetByCategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            try
            {
                //ModelHelper.
                List<Advertisement> entities = null;
                if (!string.IsNullOrEmpty(Request.QueryString.Value))
                {
                    entities = new AdvertisementHandler().GetAdvertisementsByCategory(categoryId);
                }
                return Ok(entities?.ToModelList());
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }


        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Advertisement? entity = new AdvertisementHandler().GetAdvertisement(id);
                return Ok(entity.ToModel());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post(AdvertisementModel model)
        {
            try
            {
                model.PostedBy = new UserModel { Id = 2 };
                Advertisement? entity = new AdvertisementHandler().AddAdvertisement(model.ToEntity());
                return Created($"{Request.Path}/{entity.Id}", entity.ToModel());


            }
            catch (Exception ex)
            {
                return StatusCode(500 );
            }
        }



    }
}
