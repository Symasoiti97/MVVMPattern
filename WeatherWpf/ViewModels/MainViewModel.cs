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
            TimeParse = 120;

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
                            }
                        };
                        weatherWindow.Show();
                    }
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
    }
}
