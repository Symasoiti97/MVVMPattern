using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWpf.Models
{
    public class WeatherSetting : IEquatable<WeatherSetting>, IWeatherSetting
    {
        public string MeasureTemp { get; set; }
        public string MeasurePressure { get; set; }
        //public string MeasureHumidity { get; set; }
        public string Region { get; set; }
        public int TimeParse { get; set; }

        public double MeasureTempSet(double temp)
        {
            switch (MeasureTemp)
            {
                case "C": break;
                case "F": temp = (temp * 9 / 5) + 32; break;
            }

            return temp;
        }

        public double MeasurePressureSet(double pressure)
        {
            switch (MeasurePressure)
            {
                case "pha": break;
                case "bar": pressure = pressure * 1000 / 100000; break;
            }

            return pressure;
        }

        public bool Equals(WeatherSetting other)
        {
            return Region.Equals(other.Region) && MeasureTemp.Equals(other.MeasureTemp) 
                && MeasurePressure.Equals(other.MeasurePressure) && TimeParse.Equals(other.TimeParse);
        }
    }
}
