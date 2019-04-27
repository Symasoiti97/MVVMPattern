using Api.API;
using System.Configuration;

namespace ObserverPattern.API.ApiOWM
{
    public class WeatherOWMSetting : IApiSetting
    {
        public string Url { get; }

        public WeatherOWMSetting(string Region)
        {
            string url = ConfigurationManager.AppSettings.Get("JsonOWM");

            Url = string.Format(url, Region);
        }
    }
}
