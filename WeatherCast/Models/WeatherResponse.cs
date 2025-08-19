namespace WeatherCast.Models
{
    public class WeatherResponse
    {
        public string City { get; set; }
        public string Description { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
    }
}
