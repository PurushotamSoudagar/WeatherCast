using Microsoft.AspNetCore.Mvc;
using WeatherAppMvc.Services;
using WeatherCast.Models;

namespace WeatherAppMvc.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new WeatherRequest());
        }

        [HttpPost]
        public IActionResult Index(WeatherRequest request)
        {
            if (string.IsNullOrEmpty(request.City))
            {
                ModelState.AddModelError("", "Please enter a city name.");
                return View(request);
            }

            // Redirect to Result page with city name
            return RedirectToAction("Result", new { city = request.City });
        }

        [HttpGet]
        public async Task<IActionResult> Result(string city)
        {
            var weather = await _weatherService.GetWeatherAsync(city);
            if (weather == null)
            {
                ViewBag.Error = "City not found or API error.";
                return View();
            }
            return View(weather);
        }
    }
}
