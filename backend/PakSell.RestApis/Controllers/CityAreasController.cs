using Microsoft.AspNetCore.Mvc;
using paksell;
using paksell.Db;
using PakSell.Models;
using System;
using System.Collections.Generic;

namespace PakSell.RestApis.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CityAreaController : ControllerBase
    {
        private readonly CityAreaHandler _cityAreaHandler;

        // Dependency injection
        public CityAreaController(CityAreaHandler cityAreaHandler)
        {
            _cityAreaHandler = cityAreaHandler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Fetch all city areas
                List<CityArea> cityAreas = _cityAreaHandler.GetCityAreas();

                // Return the city areas as a response
                return Ok(cityAreas?.ToList());
            }
            catch (Exception ex)
            {
                // Return internal server error if something goes wrong
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
