using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWpf.Models
{
    interface ICollectionWeatherSetting
    {
        bool Add(WeatherSetting weatherSetting);
        bool Remove(WeatherSetting weatherSetting);
    }
}
