using Api.API;
using ObserverPattern.API.ApiOWM;
using System;
using WeatherWpf.Models.WeatherApi;
using System.Configuration;
using WeatherWpf.Models;
using System.Windows.Input;
using WeatherWpf.Commands;
using System.Collections.Generic;
using System.Windows.Threading;

namespace WeatherWpf.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        private ICollectionWeatherSetting _collectionWeather;
        private ApiWorker<Weather> _apiWorker;
        private Weather _weather;
        private WeatherSetting _weatherSetting;
        private DispatcherTimer _timer;

        public WeatherViewModel()
        {
            _weather = new Weather();
            _collectionWeather = CollectionWeatherSetting.GetInstance();
        }

        public WeatherSetting WeatherSett
        {
            get
            {
                return _weatherSetting;
            }
            set
            {
                _weatherSetting = value;
                StartTimer();
            }
        }

        public ICommand ClosingWindow
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
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

        private void StartTimer()
        {
            _apiWorker = new ApiWorker<Weather>(new ApiWeatherJson(), new WeatherOWMSetting(_weatherSetting.Region));
            _apiWorker.EventStart += Show;

            int millisec = Convert.ToInt16(WeatherSett.TimeParse) * 1000;

            if (millisec < 1000)
            {
                millisec = 1000;
            }

            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _apiWorker.Parse();
        }

        private void Show(Weather obj)
        {
            Temp = WeatherSett.MeasureTempSet(obj.Temperature).ToString();
            Pressure = WeatherSett.MeasurePressureSet(obj.Pressure).ToString();
            Humidity = obj.Humidity.ToString();
        }
    }
}
