using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWpf.Models
{
    class WeatherSetting
    {
        public string MeasureTemp { get; set; }
        public string MeasurePressure { get; set; }
        public string MeasureHumidity { get; set; }
        public string City { get; set; }
        public int TimeParse { get; set; }

        public static double MeasureTempSet(double temp, string measure)
        {
            switch (measure)
            {
                case "C": break;
                case "F": temp = (temp * 9 / 5) + 32; break;
            }

            return temp;
        }

        public static double MeasurePressureSet(double pressure, string measure)
        {
            switch (measure)
            {
                case "pha": break;
                case "bar": pressure = pressure * 1000 / 100000; break;
            }

            return pressure;
        }

        public bool Equals(WeatherSetting other)
        {
            return City.Equals(other.City);
            //Решить вопрос с null
            // && MeasureTemp.Equals(other.MeasureTemp) && MeasurePressure.Equals(other.MeasurePressure) 
            //&& MeasureHumidity.Equals(other.MeasureHumidity); && TimeParse.Equals(other.TimeParse);
        }
    }
}
