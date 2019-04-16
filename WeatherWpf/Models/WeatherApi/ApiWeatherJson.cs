using Api.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace WeatherWpf.Models.WeatherApi
{
    public class ApiWeatherJson : IApi<Weather>
    {
        public Weather Parse(string reader)
        {
            JObject json = JObject.Parse(reader);

            var weatherArray = json["main"].ToString();
            
            return JsonConvert.DeserializeObject<Weather>(weatherArray);
        }
    }
}
