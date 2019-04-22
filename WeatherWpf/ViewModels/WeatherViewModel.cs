using Api.API;
using ObserverPattern.API.ApiOWM;
using System;
using WeatherWpf.Models.WeatherApi;
using System.Configuration;
using WeatherWpf.Models;
using System.Windows.Input;
using WeatherWpf.Commands;
using System.Collections.Generic;

namespace WeatherWpf.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        public WeatherViewModel()
        {
            _weather = new Weather();
            _collectionWeather = CollectionWeatherSetting.GetInstance();
        }

        private ICollectionWeatherSetting _collectionWeather;
        private ApiWorker<Weather> _apiWorker;
        private Weather _weather;
        private WeatherSetting _weatherSetting;

        public WeatherSetting WeatherSett
        {
            get
            {
                return _weatherSetting;
            }
            set
            {
                _weatherSetting = value;
                StartParse();
            }
        }

        private void StartParse()
        {

            string url = ConfigurationManager.AppSettings.Get("JsonOWM").Replace("Region", WeatherSett.Region);
            _apiWorker = new ApiWorker<Weather>(new ApiWeatherJson(), new WeatherOWMSetting(url));
            _apiWorker.EventStart += Show;

            int millisec = Convert.ToInt16(WeatherSett.TimeParse) * 1000;

            if (millisec < 1000)
            {
                millisec = 1000;
            }

            _apiWorker.Start(millisec);
        }

        private void Show(Weather obj)
        {
            Temp = WeatherSett.MeasureTempSet(obj.Temperature).ToString();
            Pressure = WeatherSett.MeasurePressureSet(obj.Pressure).ToString();
            Humidity = obj.Humidity.ToString();
        }

        public ICommand ClosingWindow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _apiWorker.Abort();
                    _collectionWeather.Remove(_weatherSetting);
                });
            }
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
