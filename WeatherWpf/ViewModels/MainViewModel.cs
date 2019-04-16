using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherWpf.Commands;
using WeatherWpf.Models;
using WeatherWpf.Views;

namespace WeatherWpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            _weatherSettings = new List<WeatherSetting>();
            MeasurePressureHpa = true;
            MeasureTempC = true;
            _measureTemp = "C";
            _measurePressure = "hpa";

        }

        private List<WeatherSetting> _weatherSettings;
        private string _textBoxCity;
        private string _measureTemp;
        private bool _measureTempC;
        private bool _measureTempF;
        private string _measurePressure;
        private bool _measurePressureHpa;
        private bool _measurePressureBar;
        private string _timeParse;

        public ICommand ButtonStart
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    WeatherSetting weatherSetting = new WeatherSetting
                    {
                        City = _textBoxCity,
                        MeasureTemp = _measureTemp
                    };

                    if (_weatherSettings.Contains<WeatherSetting>(weatherSetting)) return;
                    _weatherSettings.Add(weatherSetting);

                    var iv = new WeatherWindow()
                    {
                        DataContext = new WeatherViewModel
                        {
                            City = _textBoxCity,
                            MeasureTemp = _measureTemp,
                            MeasurePressure = _measurePressure,
                            TimeParse = _timeParse
                        }
                    };
                    iv.Show();
                });
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

        public string TextBoxCity
        {
            get
            {
                return _textBoxCity;
            }
            set
            {
                _textBoxCity = value;
                OnPropertyChanged("TextBoxCity");
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

        public string TimeParse
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
    }
}
