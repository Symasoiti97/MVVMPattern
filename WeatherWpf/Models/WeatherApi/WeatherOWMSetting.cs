using Api.API;
using System.Configuration;

namespace ObserverPattern.API.ApiOWM
{
    public class WeatherOWMSetting : IApiSetting
    {
        public string Url { get; }

        public WeatherOWMSetting(string Region)
        {
            string url = ConfigurationManager.AppSettings.Get("JsonOWM").Replace("Region", Region);

            Url = string.Format(url, Region);
        }
    }
}
