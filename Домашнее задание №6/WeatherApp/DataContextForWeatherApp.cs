using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Brushes
    {

        public LinearGradientBrush Day_Normal_Brush;
        public LinearGradientBrush Cloud_Brush;
        public LinearGradientBrush Night_Brush;
        public LinearGradientBrush SunsetOrSunrise_Brush;
        public LinearGradientBrush FG_Day_Normal_Brush;
        public LinearGradientBrush FG_Cloud_Brush;
        public LinearGradientBrush FG_Night_Brush;
        public LinearGradientBrush FG_SunsetOrSunrise_Brush;

        public LinearGradientBrush Ligher_Day_Normal_Brush;
        public LinearGradientBrush Lighter_Cloud_Brush;
        public LinearGradientBrush Lighter_Night_Brush;
        public LinearGradientBrush LighterSunsetOrSunrise_Brush;
        public LinearGradientBrush LFG_Day_Normal_Brush;
        public LinearGradientBrush LFG_Cloud_Brush;
        public LinearGradientBrush LFG_Night_Brush;
        public LinearGradientBrush LFG_SunsetOrSunrise_Brush;

        public Brushes()
        {
            Day_Normal_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 72, 214, 243), 0),
                new GradientStop(new Color(255, 51, 109, 207), 1)
            }
            };
            Ligher_Day_Normal_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 120, 201, 241), 1),
                new GradientStop(new Color(255, 169, 240, 255), 0)
            }
            };
            FG_Day_Normal_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 233, 255, 238), 1),
                new GradientStop(new Color(255, 255, 255, 255), 0)
            }
            };
            LFG_Day_Normal_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 233, 255, 238), 1),
                new GradientStop(new Color(255, 255, 255, 255), 0)
            }
            };


            Cloud_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 111, 111, 111), 0),
                new GradientStop(new Color(255, 196, 196, 196), 1)
            }
            };
            Lighter_Cloud_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 242, 242, 242), 0),
                new GradientStop(new Color(255, 202, 202, 202), 1)
            }
            };
            FG_Cloud_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 233, 255, 238), 1),
                new GradientStop(new Color(255, 255, 255, 255), 0)
            }
            };
            LFG_Cloud_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 55, 55, 138), 1),
                new GradientStop(new Color(255, 155, 155, 155), 0)
            }
            };


            Night_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {               
                new GradientStop(new Color(255, 21, 42, 160), 0),
                new GradientStop(new Color(255, 29, 0, 92), 1)
            }
            };
            Lighter_Night_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {                
                new GradientStop(new Color(255, 91, 102, 244), 0),
                new GradientStop(new Color(255, 54, 24, 178), 1)
            }
            };
            FG_Night_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 255, 255, 177), 1),
                new GradientStop(new Color(255, 255, 255, 255), 0)
            }
            };
            LFG_Night_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 255, 255, 225), 1),
                new GradientStop(new Color(255, 255, 255, 255), 0)
            }
            };


            SunsetOrSunrise_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 255, 166, 48), 0),
                new GradientStop(new Color(255, 255, 179, 149), 1)
            }
            };
            LighterSunsetOrSunrise_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 255, 234, 170), 0),
                new GradientStop(new Color(255, 255, 117, 117), 1)
            }
            };

            FG_SunsetOrSunrise_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 255, 255, 255), 1),
                new GradientStop(new Color(255, 255, 255, 255), 0)
            }
            };
            LFG_SunsetOrSunrise_Brush = new LinearGradientBrush
            {
                GradientStops = new GradientStops
            {
                new GradientStop(new Color(255, 255, 255, 255), 1),
                new GradientStop(new Color(255, 255, 255, 255), 0)
            }
            };

        }
    }
    class SubIcon
    {   
        public string Time { get; set; }
        public string Temperature { get; set; }
        public Bitmap Icon { get; set; }

        public SubIcon(string time, string temperature, Bitmap icon) 
        {
            Time = time;
            Temperature = temperature;
            Icon = icon;
        }
    }

    class WeatherLine
    {
        public string Date { get; set; }
        public ObservableCollection<SubRow> Rows { get; set; }
        public WeatherLine(string date, ObservableCollection<SubRow> rows) 
        {
            Date = date;
            Rows = rows;
        }

    }
    class SubRow
    {
        public string Part_of_day { get; set; }
        public string Temperature { get; set; }
        public string Min_City_temperature { get; set; }
        public string Max_City_temperature { get; set; }
        public Bitmap Icon { get; set; }
        public string Icon_description { get; set; }    
        public string Humidity { get; set; }
        public string Wind { get; set; }
        

        public SubRow(string pod, string temperature, string mintemp, string maxtemp, Bitmap icon, string desc, string humid, string wind)
        {
            Part_of_day = pod;
            Temperature = temperature;
            Min_City_temperature = mintemp;
            Max_City_temperature = maxtemp;
            Icon = icon;
            Icon_description = desc;
            Humidity = humid;
            Wind = wind;
        }
    }

    internal class DataContextForWeatherApp : INotifyPropertyChanged
    {
        public enum Temp_Settings : int
        {
            CELSIUS,
            FAHRENHEIT,
            KELVIN
        }
        public enum ThemeSetting : int
        {
            AUTO,
            SUNRISE,
            NIGHT,
            DAY,
            CLOUDY
        }
        public int ThemeSetting_val { get => _themesetting_val; set => SetField(ref _themesetting_val, value); }
        public void SetTemp(int val)
        { Temperaturemode = val; DataContext_updateAsync(); }
        private int _themesetting_val;

        static public bool workflag { get => _workflag; }
        static private bool _workflag = false;
        private FileSystemWatcher? _watcher;
        public int Temperaturemode { get => _tempmode; set => SetField(ref _tempmode, value); }
        private int _tempmode;
        private Forecast _forecast;
        private string _date;
        private string _cityName;
        private string _current_temperature;
        private Bitmap _current_weather_icon;
        private string _current_weather_name;
        private string _humidity;
        private string _pressure;
        private string _wind;
        private string _gusts;
        private string _visibility;
        private string _POP;
        private string _sunrise;
        private string _sunset;
        private LinearGradientBrush _brush;
        private LinearGradientBrush _lighterbrush;
        private LinearGradientBrush _FGbrush;
        private LinearGradientBrush _LFGbrush;
        private Brushes _brushes;
        private ObservableCollection<SubIcon> _subIconCollection;
        private ObservableCollection<WeatherLine> _weatherLineCollection;

        public LinearGradientBrush Brush
        {
            get => _brush;
            set => SetField(ref _brush, value);
        }
        public LinearGradientBrush LighterBrush
        {
            get => _lighterbrush;
            set => SetField(ref _lighterbrush, value);
        }
        public LinearGradientBrush FGBrush
        {
            get => _FGbrush;
            set => SetField(ref _FGbrush, value);
        }
        public LinearGradientBrush LFGBrush
        {
            get => _LFGbrush;
            set => SetField(ref _LFGbrush, value);
        }
        public string Date
        {
            get => _date;
            set => SetField(ref _date, value);
        }

        public string CityName
        {
            get => _cityName;
            set => SetField(ref _cityName, value);
        }
        public string Current_temperature
        {
            get => _current_temperature;
            set
            {
                double temperature = Convert.ToDouble(value);
                SetField(ref _current_temperature, Temperature_Converter(temperature));
            }
        }
        public Bitmap Current_weather_icon
        {
            get => _current_weather_icon;
            set => SetField(ref _current_weather_icon, value);
        }
        public string Current_weather_name
        {
            get => _current_weather_name;
            set => SetField(ref _current_weather_name, value);
        }
        public string Humidity
        {
            get => _humidity;
            set => SetField(ref _humidity, value);
        }
        public string Pressure
        {
            get => _pressure;
            set => SetField(ref _pressure, value);
        }
        public string Wind
        {
            get => _wind;
            set => SetField(ref _wind, value);
        }
        public string Gusts
        {
            get => _gusts;
            set => SetField(ref _gusts, value);
        }
        public string Visibility
        {
            get => _visibility;
            set => SetField(ref _visibility, value);
        }
        private string _H3;
        public string H3
        {
            get => _H3;
            set => SetField(ref _H3, value);
        }
        private string _All;
        public string All
        {
            get => _All;
            set => SetField(ref _All, value);
        }
        public string POP
        {
            get => _POP;
            set => SetField(ref _POP, value);
        }
        public string Sunrise
        {
            get => _sunrise;
            set => SetField(ref _sunrise, value);
        }
        public string Sunset
        {
            get => _sunset;
            set => SetField(ref _sunset, value);
        }
        public ObservableCollection<SubIcon> SubIconCollection
        {
            get => _subIconCollection;
            set => SetField(ref _subIconCollection, value);
        }
        public ObservableCollection<WeatherLine> WeatherLines
        {
            get => _weatherLineCollection;
            set => SetField(ref _weatherLineCollection, value);
        }

        public DataContextForWeatherApp()
        {
            Console.WriteLine("Initialized datacontext");
            _forecast = new Forecast();
            _brushes = new Brushes();
            ThemeSetting_val = (int)ThemeSetting.AUTO;
            Temperaturemode = ((int)Temp_Settings.CELSIUS);        
            DataContext_updateAsync();
            SetTheme();
            Watcher_init();
        }
        public void Watcher_init()
        {
            _watcher = new FileSystemWatcher(@".\files", "forecast.json");
            _watcher.Changed += (sender, e) => { watcher_action(); };
            _watcher.EnableRaisingEvents = true;
        }
        public void watcher_action()
        {
            Date = "Wait a sec, Loading...";
            Task.Run(DataContext_updateAsync);
        }

        public void SetTheme(int val)
        { ThemeSetting_val = val; SetTheme(); }
        private void SetTheme()
        {
            
            if (_forecast == null || _forecast.List == null || _forecast.List.Count == 0)
            {
                Brush = _brushes.SunsetOrSunrise_Brush;
                LighterBrush = _brushes.LighterSunsetOrSunrise_Brush;
                FGBrush = _brushes.FG_SunsetOrSunrise_Brush;
                LFGBrush = _brushes.LFG_SunsetOrSunrise_Brush;
            }
            else
            {
                WeatherForecast currweather = _forecast.List[0];
                if (ThemeSetting_val == (int)ThemeSetting.AUTO)
                {
                    if (currweather.Weather[0].Main != "Clear" && currweather.Clouds.All > 45)
                    {
                        Brush = _brushes.Cloud_Brush;
                        LighterBrush = _brushes.Lighter_Cloud_Brush;
                        FGBrush = _brushes.FG_Cloud_Brush;
                        LFGBrush = _brushes.LFG_Cloud_Brush;
                        Console.WriteLine("1.33");
                    }
                    else
                    {
                        switch (FindPod(DateTimeOffset.Now.ToUnixTimeSeconds()))
                        {
                            case "Morning":

                                Brush = _brushes.SunsetOrSunrise_Brush;
                                LighterBrush = _brushes.LighterSunsetOrSunrise_Brush;
                                FGBrush = _brushes.FG_SunsetOrSunrise_Brush;
                                LFGBrush = _brushes.LFG_SunsetOrSunrise_Brush;
                                break;
                            case "Evening":
                                Brush = _brushes.SunsetOrSunrise_Brush;
                                LighterBrush = _brushes.LighterSunsetOrSunrise_Brush;
                                FGBrush = _brushes.FG_SunsetOrSunrise_Brush;
                                LFGBrush = _brushes.LFG_SunsetOrSunrise_Brush;
                                break;
                            case "Day":

                                Brush = _brushes.Day_Normal_Brush;
                                LighterBrush = _brushes.Ligher_Day_Normal_Brush;
                                FGBrush = _brushes.FG_Day_Normal_Brush;
                                LFGBrush = _brushes.LFG_Day_Normal_Brush;
                                break;
                            case "Night":

                                Brush = _brushes.Night_Brush;
                                LighterBrush = _brushes.Lighter_Night_Brush;
                                FGBrush = _brushes.FG_Night_Brush;
                                LFGBrush = _brushes.LFG_Night_Brush;
                                break;
                            default:
                                Brush = _brushes.Day_Normal_Brush;
                                LighterBrush = _brushes.Ligher_Day_Normal_Brush;
                                FGBrush = _brushes.FG_Day_Normal_Brush;
                                LFGBrush = _brushes.LFG_Day_Normal_Brush;
                                break;
                        }
                    }
                }
                else if (ThemeSetting_val == (int)ThemeSetting.CLOUDY)
                {
                    Brush = _brushes.Cloud_Brush;
                    LighterBrush = _brushes.Lighter_Cloud_Brush;
                    FGBrush = _brushes.FG_Cloud_Brush;
                    LFGBrush = _brushes.LFG_Cloud_Brush;
                }
                else if (ThemeSetting_val == (int)ThemeSetting.DAY)
                {
                    Brush = _brushes.Day_Normal_Brush;
                    LighterBrush = _brushes.Ligher_Day_Normal_Brush;
                    FGBrush = _brushes.FG_Day_Normal_Brush;
                    LFGBrush = _brushes.LFG_Day_Normal_Brush;
                }
                else if (ThemeSetting_val == (int)ThemeSetting.NIGHT)
                {
                    Brush = _brushes.Night_Brush;
                    LighterBrush = _brushes.Lighter_Night_Brush;
                    FGBrush = _brushes.FG_Night_Brush;
                    LFGBrush = _brushes.LFG_Night_Brush;
                }
                else
                {
                    Brush = _brushes.SunsetOrSunrise_Brush;
                    LighterBrush = _brushes.LighterSunsetOrSunrise_Brush;
                    FGBrush = _brushes.FG_SunsetOrSunrise_Brush;
                    LFGBrush = _brushes.LFG_SunsetOrSunrise_Brush;
                }
            }
        }

        public async Task DataContext_updateAsync()
        {
            Console.WriteLine("1");
            _workflag = true;
            if (!File.Exists(@".\files\forecast.json"))
            {
                if (File.Exists(@".\files\geocode.json"))
                {
                    await MakeRequest.Make_a_Request_Geocode();
                }
            }
            _forecast = await JSON_Handler.ReadJSONAsync<Forecast>(@".\files\forecast.json");
            Console.WriteLine("1.2");
            if (_forecast != null && _forecast.List[0] != null && _forecast.List[0].Weather[0] != null)
            {
                var currweather = _forecast.List[0];
                Console.WriteLine("1.3");
                SetTheme();

                //
                //
                // Top panel update
                Date = DateTimeOffset.FromUnixTimeSeconds(currweather.Dt).DayOfWeek.ToString() +
                    ", " + DateTimeOffset.FromUnixTimeSeconds(currweather.Dt).DateTime.ToString("MMMM", CultureInfo.InvariantCulture) +
                    " " + DateTimeOffset.FromUnixTimeSeconds(currweather.Dt).Day.ToString();
                var position = await JSON_Handler.ReadJSONAsync<List<Location>>(@".\files\geocode.json");
                if (position != null)
                {
                    CityName = "";
                    if (position[0].Name != null && position[0].State != null && position[0].Country != null)
                    {
                        CityName = position[0].Name + ", " + position[0].State + " (" + position[0].Country + ")";
                    }
                    else if (position[0].Name != null && position[0].Country != null)
                    {
                        CityName = position[0].Name + " (" + position[0].Country + ")";
                    }
                    else if (position[0].Name != null && position[0].State != null)
                    {
                        CityName = position[0].Name + ", " + position[0].State;
                    }
                    else if (position[0].Name != null)
                    {
                        CityName = position[0].Name;
                    }
                    else
                    {
                        CityName = _forecast.City.Name;
                    }
                }
                else
                {
                    CityName = _forecast.City.Name;
                }
                Current_temperature = currweather.Main.Temp.ToString();
                Current_weather_icon = new Bitmap(@".\resources\" + currweather.Weather[0].Icon + "@4x.png");
                Current_weather_name = currweather.Weather[0].Main;
                Console.WriteLine("3");
                //
                //
                // SubIconCollection Update (the hourly thing thing)
                //Console.WriteLine("_forecast.list.count: " + _forecast.List.Count);
                SubIcon[] list = new SubIcon[_forecast.List.Count];
                list[0] = (new SubIcon("Now",
                    Temperature_Converter(_forecast.List[0].Main.Temp),
                    new Bitmap(@".\resources\" + _forecast.List[0].Weather[0].Icon + "@4x.png")));
                
                for (int i = 1; i < _forecast.List.Count; i++)
                {
                    list[i] = (new SubIcon(DateTimeOffset.FromUnixTimeSeconds(_forecast.List[i].Dt).ToLocalTime().Hour.ToString() + ":00",
                        Temperature_Converter(_forecast.List[i].Main.Temp),
                        new Bitmap(@".\resources\" + _forecast.List[i].Weather[0].Icon + "@4x.png")));
                    //Console.WriteLine(list[i].Time + list[i].Temperature + list[i].Icon);
                }
                SubIconCollection = new ObservableCollection<SubIcon>(list);

                Console.WriteLine("4");
                //
                //
                // Additional weather info update
                Humidity = currweather.Main.Humidity.ToString() + "%";
                Pressure = currweather.Main.Pressure.ToString() + " hPa";
                Wind = currweather.Wind.Speed.ToString() + "m/s, " + WindDirection(currweather.Wind.Deg);
                Gusts = currweather.Wind.Gust.ToString() + "m/s, " + WindDirection(currweather.Wind.Deg);
                Visibility = currweather.Visibility.ToString() + " meters";              
                if (currweather.Rain != null)
                {
                    H3 = currweather.Rain.H3.ToString() + " mm";
                }
                else if (currweather.Snow != null) 
                {
                    H3 = currweather.Snow.H3.ToString() + " mm";
                }
                else
                {
                    H3 = "0 mm";
                }
                All = currweather.Clouds.All.ToString() + "%";
                POP = (currweather.Pop * 100).ToString() + "%";
                Sunrise = DateTimeOffset.FromUnixTimeSeconds(_forecast.City.Sunrise).LocalDateTime.TimeOfDay.ToString();
                Sunset = DateTimeOffset.FromUnixTimeSeconds(_forecast.City.Sunset).LocalDateTime.TimeOfDay.ToString();

                //
                //
                // Weather Lines
                WeatherLine[] weathers = new WeatherLine[5]; // 5 days forecast
                long secs = _forecast.List[0].Dt;
                for (int day = 0; day < 5; day++)
                {
                    SubRow[] listsr = new SubRow[4]; // 4 rows
                    for (int i = 0; i < listsr.Length; i++)
                    {
                        secs = _forecast.List[2 * i + 8 * day].Dt; // 2 * i == move 6 hours, 8 * i = move a day
                        listsr[i] = new SubRow(FindPod(secs),
                            Temperature_Converter(_forecast.List[2 * i + 8 * day].Main.Temp),
                            Temperature_Converter(_forecast.List[2 * i + 8 * day].Main.TempMin),
                            Temperature_Converter(_forecast.List[2 * i + 8 * day].Main.TempMax),
                            new Bitmap(@".\resources\" + _forecast.List[2 * i + 8 * day].Weather[0].Icon + "@4x.png"),
                            _forecast.List[2 * i + 8 * day].Weather[0].Main, _forecast.List[2 * i + 8 * day].Main.Humidity.ToString() + "%",
                            _forecast.List[2 * i + 8 * day].Wind.Speed.ToString() + "m/s, " + WindDirection(_forecast.List[2 * i + 8 * day].Wind.Deg));

                    }
                    weathers[day] = new WeatherLine(Date = DateTimeOffset.FromUnixTimeSeconds(_forecast.List[8 * day].Dt).DayOfWeek.ToString() +
                    ", " + DateTimeOffset.FromUnixTimeSeconds(_forecast.List[8 * day].Dt).DateTime.ToString("MMMM", CultureInfo.InvariantCulture) +
                    " " + DateTimeOffset.FromUnixTimeSeconds(_forecast.List[8 * day].Dt).Day.ToString(),
                    new ObservableCollection<SubRow>(listsr));
                }
                WeatherLines = new ObservableCollection<WeatherLine>(weathers);
                _workflag = false;
                Console.WriteLine("5");
            }
            else throw new Exception("Didn't load properly");
        }
        private string FindPod(long dt)
        {
            int stringhourreference = DateTimeOffset.FromUnixTimeSeconds(dt).ToLocalTime().Hour;
            if (stringhourreference >= 6 && stringhourreference <= 11){ return "Morning"; }
            else if (stringhourreference >= 12 && stringhourreference <= 17) { return "Day"; }
            else if (stringhourreference >= 18 && stringhourreference <= 23) { return "Evening"; }
            else if (stringhourreference >= 00 && stringhourreference <= 5) { return "Night"; }
            else return "idk";
        }
        private string Temperature_Converter(double temperature)
        {
            string temp;
            switch (Temperaturemode)
            {
                case ((int)Temp_Settings.CELSIUS):
                    temperature -= 273.15;
                    temperature = Math.Round(temperature, 1);
                    temp = temperature >= 0 ? $"+{temperature}" + "\u00B0" : $"{temperature}" + "\u00B0";
                    break;
                case ((int)Temp_Settings.FAHRENHEIT):
                    temperature = (temperature - 273.15) * 1.8 + 32;
                    temperature = Math.Round(temperature, 1);
                    temp = temperature >= 0 ? $"+{temperature}" + "\u00B0" : $"{temperature}" + "\u00B0";
                    break;
                default:
                    temperature = Math.Round(temperature, 1);
                    temp = temperature >= 0 ? $"+{temperature}" + "\u00B0" : $"{temperature}" + "\u00B0";
                    break;
            }
            return temp;
        }

    private string WindDirection(int wind_Degree)
        {
            if ((wind_Degree >= 0 && wind_Degree <= 30) || (wind_Degree > 330 && wind_Degree <= 360))
            {
                return "N";
            }
            else if (wind_Degree > 30 && wind_Degree <= 60)
            {
                return "NE";
            }
            else if (wind_Degree > 60 && wind_Degree <= 120)
            {
                return "E";
            }
            else if (wind_Degree > 120 && wind_Degree <= 150)
            {
                return "SE";
            }
            else if (wind_Degree > 150 && wind_Degree <= 210)
            {
                return "S";
            }
            else if (wind_Degree > 210 && wind_Degree <= 240)
            {
                return "SW";
            }
            else if (wind_Degree > 240 && wind_Degree <= 300)
            {
                return "W";
            }
            else if (wind_Degree > 300 && wind_Degree <= 330)
            {
                return "NW";
            }
            else
            {
                return "lol idk";
            }
        }







        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
