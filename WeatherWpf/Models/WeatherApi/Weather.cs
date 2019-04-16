using Newtonsoft.Json;

namespace WeatherWpf.Models.WeatherApi
{
    public class Weather
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        [JsonProperty("humidity")]
        public double Humidity { get; set; }
        [JsonProperty("pressure")]
        public double Pressure { get; set; }
    }
}
