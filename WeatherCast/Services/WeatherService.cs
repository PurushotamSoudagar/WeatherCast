using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherCast.Models;

namespace WeatherAppMvc.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "f69ceee362669dc59cd599f54d49e49d";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse?> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);

            var root = doc.RootElement;
            return new WeatherResponse
            {
                City = root.GetProperty("name").GetString(),
                Description = root.GetProperty("weather")[0].GetProperty("description").GetString(),
                Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                FeelsLike = root.GetProperty("main").GetProperty("feels_like").GetDouble()
            };
        }
    }
}
