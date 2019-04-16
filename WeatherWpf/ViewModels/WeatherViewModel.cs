using Api.API;
using ObserverPattern.API.ApiOWM;
using System;
using WeatherWpf.Models.WeatherApi;
using System.Configuration;
using WeatherWpf.Models;

namespace WeatherWpf.ViewModels
{
    class WeatherViewModel : BaseViewModel
    {
        public WeatherViewModel()
        {
            _weather = new Weather();
            WeatherSett = new WeatherSetting();
        }

        private Weather _weather;

        public WeatherSetting WeatherSett { get; set; }

        private void StartParse()
        {
            string url = ConfigurationManager.AppSettings.Get("JsonOWM").Replace("Region", WeatherSett.Region); ;
            ApiWorker<Weather> apiWorker = new ApiWorker<Weather>(new ApiWeatherJson(), new WeatherOWMSetting(url));
            apiWorker.EventStart += Show;

            int millisec = Convert.ToInt16(WeatherSett.TimeParse) * 1000;

            if (millisec < 1000)
            {
                millisec = 1000;
            }

            apiWorker.Start(millisec);
        }

        private void Show(Weather obj)
        {
            Temp = WeatherSetting.MeasureTempSet(obj.Temperature, WeatherSett.MeasureTemp).ToString();
            Pressure = WeatherSetting.MeasurePressureSet(obj.Pressure, WeatherSett.MeasurePressure).ToString();
            Humidity = obj.Humidity.ToString();
        }

        public string Region
        {
            get
            {
                return WeatherSett.Region;
            }
            set
            {
                WeatherSett.Region = value;
                OnPropertyChanged("Region");
                StartParse();
            }
        }

        public string Temp
        {
            get
            {
                return $"Temperature: {_weather.Temperature.ToString()} {WeatherSett.MeasureTemp}";
            }
            set
            {
                _weather.Temperature = Convert.ToDouble(value);
                OnPropertyChanged("Temp");
            }
        }

        public string Pressure
        {
            get
            {
                return $"Pressure: {_weather.Pressure.ToString()} {WeatherSett.MeasurePressure}";
            }
            set
            {
                _weather.Pressure = Convert.ToDouble(value);
                OnPropertyChanged("Pressure");
            }
        }

        public string Humidity
        {
            get
            {
                return $"Humidity: {_weather.Humidity.ToString()} %";
            }
            set
            {
                _weather.Humidity = Convert.ToDouble(value);
                OnPropertyChanged("Humidity");
            }
        }
    }
}
