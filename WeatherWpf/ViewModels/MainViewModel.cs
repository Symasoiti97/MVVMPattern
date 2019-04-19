using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherWpf.Commands;
using WeatherWpf.Models;
using WeatherWpf.Validation;
using WeatherWpf.Views;

namespace WeatherWpf.ViewModels
{
    public class MainViewModel : BaseViewModel, IDataErrorInfo
    {
        public MainViewModel()
        {
            _weatherSettings = new List<WeatherSetting>();
            MeasurePressureHpa = true;
            MeasureTempC = true;
            _measureTemp = "C";
            _measurePressure = "hpa";
            TimeParse = 120;
            TextBoxRegion = "";
        }

        private List<WeatherSetting> _weatherSettings;
        private string _textBoxRegion;
        private string _measureTemp;
        private bool _measureTempC;
        private bool _measureTempF;
        private string _measurePressure;
        private bool _measurePressureHpa;
        private bool _measurePressureBar;
        private int _timeParse;

        public ICommand ButtonStart
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OpenWeatherWeapon();
                });
            }
        }

        private void OpenWeatherWeapon()
        {
            WeatherSetting weatherSetting = new WeatherSetting
            {
                Region = TextBoxRegion,
                MeasureTemp = _measureTemp,
                MeasurePressure = _measurePressure,
                TimeParse = TimeParse
            };

            if (!_weatherSettings.Contains<WeatherSetting>(weatherSetting))
            {
                _weatherSettings.Add(weatherSetting);

                var weatherWindow = new WeatherWindow()
                {
                    DataContext = new WeatherViewModel
                    {
                        WeatherSett = weatherSetting,
                        WeatherSettings = _weatherSettings
                    }
                };
                weatherWindow.Show();
            }
        }

        public ICommand MeasureTempFClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MeasureTempF = true;
                    MeasureTempC = false;
                    _measureTemp = "F";
                });
            }
        }

        public ICommand MeasureTempCClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MeasureTempC = true;
                    MeasureTempF = false;
                    _measureTemp = "C";
                });
            }
        }

        public ICommand MeasurePressureHpaClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MeasurePressureHpa = true;
                    MeasurePressureBar = false;
                    _measurePressure = "hpa";
                });
            }
        }

        public ICommand MeasurePressureBarClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MeasurePressureBar = true;
                    MeasurePressureHpa = false;
                    _measurePressure = "bar";
                });
            }
        }

        public string TextBoxRegion
        {
            get
            {
                return _textBoxRegion;
            }
            set
            {
                _textBoxRegion = value;
                OnPropertyChanged("TextBoxRegion");
            }
        }

        public bool MeasureTempC
        {
            get
            {
                return _measureTempC;
            }
            set
            {
                _measureTempC = value;
                OnPropertyChanged("MeasureTempC");
            }
        }

        public bool MeasureTempF
        {
            get
            {
                return _measureTempF;
            }
            set
            {
                _measureTempF = value;
                OnPropertyChanged("MeasureTempF");
            }
        }

        public bool MeasurePressureHpa
        {
            get
            {
                return _measurePressureHpa;
            }
            set
            {
                _measurePressureHpa = value;
                OnPropertyChanged("MeasurePressureHpa");
            }
        }

        public bool MeasurePressureBar
        {
            get
            {
                return _measurePressureBar;
            }
            set
            {
                _measurePressureBar = value;
                OnPropertyChanged("MeasurePressureBar");
            }
        }

        public int TimeParse
        {
            get
            {
                return _timeParse;
            }
            set
            {
                _timeParse = value;
                OnPropertyChanged("TimeParse");
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;

                switch (columnName)
                {
                    case "TextBoxRegion":
                        if (TextBoxRegion == "")
                        {
                            error = "Region is null";
                        }
                        break;

                    case "TimeParse":
                        if (TimeParse < 1)
                        {
                            error = "TimeParse < 1";
                        }
                        break;
                }

                return Error;
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
