using Microsoft.AspNetCore.Mvc;
using CodeChallenge.Logic;
using CodeChallenge.DTOs;
using System.Diagnostics;
using CodeChallenge.Attributes;

namespace Code_Challenge.Controller
{
    [ApiController]
    [Route("/api/weather")]
    [StandardResponse]
    public class WeatherController : ControllerBase
    {

        private readonly WeatherLogicManager _weatherLogicManager;
        public WeatherController(WeatherLogicManager weatherLogicManager)
        {
            _weatherLogicManager = weatherLogicManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(float longitude, float latitude)
        {
            var result = await _weatherLogicManager.GetWeatherAsync(longitude, latitude);
            return StatusCode(result.StatusCode, result);
        }
    }
}