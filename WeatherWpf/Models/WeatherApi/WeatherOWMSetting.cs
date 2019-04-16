using Api.API;

namespace ObserverPattern.API.ApiOWM
{
    public class WeatherOWMSetting : IApiSetting
    {
        public string Url { get; }

        public WeatherOWMSetting(string url)
        {
            Url = url;
        }
    }
}
