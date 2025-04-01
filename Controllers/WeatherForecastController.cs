using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherForecast.Models;   // تأكد من أن هذا الـ namespace يشير إلى النموذج الصحيح
using WeatherForecast.Services; // تأكد من أن هذا الـ namespace يشير إلى خدمة الطقس

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        // Endpoint لجلب بيانات الطقس بناءً على اسم المدينة
        // GET: api/weather?city=London
        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("يرجى تزويد اسم المدينة.");

            try
            {
                var weatherData = await _weatherService.GetWeatherAsync(city);
                return Ok(weatherData);
            }
            catch (System.Exception ex)
            {
                // يمكنك تحسين التعامل مع الأخطاء هنا
                return StatusCode(500, $"حدث خطأ: {ex.Message}");
            }
        }
    }
}
