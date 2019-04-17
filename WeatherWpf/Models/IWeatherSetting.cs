using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWpf.Models
{
    interface IWeatherSetting
    {
        double MeasureTempSet(double temp);
        double MeasurePressureSet(double pressure);
    }
}
