using Api.API;
using ObserverPattern.API.ApiOWM;
using System;
using WeatherWpf.Models.WeatherApi;
using System.Configuration;
using WeatherWpf.Models;

namespace WeatherWpf.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        public WeatherViewModel()
        {
            _weather = new Weather();
        }

        private string _city;
        private Weather _weather;
        public string MeasureTemp { get; set; }
        public string MeasurePressure { get; set; }
        public string TimeParse { get; set; }

        private void Show(Weather obj)
        {
            Temp = WeatherSetting.MeasureTempSet(obj.Temperature, MeasureTemp).ToString();
            Pressure = WeatherSetting.MeasurePressureSet(obj.Pressure, MeasurePressure).ToString();
            Humidity = obj.Humidity.ToString();
        }

        private void StartParse()
        {
            string url = ConfigurationManager.AppSettings.Get("JsonOWM").Replace("City", City); ;
            ApiWorker<Weather> apiWorker = new ApiWorker<Weather>(new ApiWeatherJson(), new WeatherOWMSetting(url));
            apiWorker.EventStart += Show;
            int millisec;

            if (TimeParse == null)
            {
                millisec = 30000;
            }
            else
            {
                millisec = Convert.ToInt16(TimeParse) * 1000;
            }

            apiWorker.Start(millisec);
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged("City");
                if (City == "Anna Pavlovskaya")
                {

                }
                else
                {
                    StartParse();
                }
            }
        }

        public string Temp
        {
            get
            {
                if (City == "Anna Pavlovskaya")
                {
                    return "Temperature: 36.6 " + MeasureTemp;
                }
                else
                {
                    return "Temperature: " + _weather.Temperature.ToString() + " " + MeasureTemp;
                }
            }
            set
            {
                _weather.Temperature = Convert.ToDouble(value);
                OnPropertyChanged("Temp");
            }
        }

        public string Humidity
        {
            get
            {
                if (City == "Anna Pavlovskaya")
                {
                    return "Humidity: 30 %";
                }
                else
                {
                    return "Humidity: " + _weather.Humidity.ToString() + " %";
                }
            }
            set
            {
                _weather.Humidity = Convert.ToDouble(value);
                OnPropertyChanged("Humidity");
            }
        }

        public string Pressure
        {
            get
            {
                if (City == "Anna Pavlovskaya")
                {
                    return "Pressure: Normal";
                }
                else
                {
                    return "Pressure: " + _weather.Pressure.ToString() + " " + MeasurePressure;
                }
            }
            set
            {
                _weather.Pressure = Convert.ToDouble(value);
                OnPropertyChanged("Pressure");
            }
        }
    }
}
