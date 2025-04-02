using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherForecast.Models;   
using WeatherForecast.Services; 

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly RecentCityService _recentCityService;


        public WeatherController(WeatherService weatherService , RecentCityService recentCityService)
        {
            _weatherService = weatherService;
            _recentCityService = recentCityService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] string city , [FromQuery] string lang = "en")
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("يرجى تزويد اسم المدينة.");

            try
            {
                var weatherData = await _weatherService.GetWeatherAsync(city , lang);
                _recentCityService.AddCity(city);

                return Ok(weatherData);
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(500, $"حدث خطأ: {ex.Message}");
            }
        }
        [HttpGet("recent")]
        public IActionResult GetRecentCities()
        {
            var cities = _recentCityService.GetRecentCities();
            return Ok(cities); 
        }
        [HttpGet("location")]
        public async Task<IActionResult> GetWeatherByLocation([FromQuery] double lat, [FromQuery] double lon, [FromQuery] string lang = "ar")
        {
            var weatherData = await _weatherService.GetWeatherByCoordinatesAsync(lat, lon, lang);
            return Ok(weatherData);
        }


    }
}
