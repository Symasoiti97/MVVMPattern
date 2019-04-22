using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWpf.Models
{
    class CollectionWeatherSetting : ICollectionWeatherSetting
    {
        private static CollectionWeatherSetting _weatherSetting;
        private List<WeatherSetting> _listWeatherSettings;

        private CollectionWeatherSetting()
        {
            _listWeatherSettings = new List<WeatherSetting>();
        }

        public static CollectionWeatherSetting GetInstance()
        {
            if (_weatherSetting == null)
            {
                _weatherSetting = new CollectionWeatherSetting();
            }

            return _weatherSetting;
        }

        public bool Add(WeatherSetting weatherSetting)
        {
            bool flag;

            if (!CheckItem(weatherSetting))
            {
                _listWeatherSettings.Add(weatherSetting);
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        public bool Remove(WeatherSetting weatherSetting)
        {
            bool flag;

            if (CheckItem(weatherSetting))
            {
                _listWeatherSettings.Remove(weatherSetting);
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        private bool CheckItem(WeatherSetting weatherSetting)
        {
            bool flag;

            if (_listWeatherSettings.Contains<WeatherSetting>(weatherSetting))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }
    }
}
