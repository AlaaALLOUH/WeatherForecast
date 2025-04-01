using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
