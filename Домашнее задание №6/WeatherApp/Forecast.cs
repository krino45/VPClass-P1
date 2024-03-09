using System;
using System.Collections;
using System.Collections.Generic;

namespace WeatherApp
{
    public class Forecast
    {
        public List<WeatherForecast> List { get; set; } // List of forecasts
        public City City { get; set; } // City params 

        public override string ToString()
        {
            string retvalue;
            retvalue = "Forecast list: \n{";
            foreach (WeatherForecast listelement in List)
            {
                retvalue += listelement.ToString();
            }
            retvalue += "}\n" + "City: \n" + City.ToString();
            return retvalue; 
        }
    }

    public class WeatherForecast
    {
        public long Dt { get; set; } // date of prediction
        public MainInfo Main { get; set; } // temperature and stuff
        public List<Weather> Weather { get; set; } //  weather conds
        public Clouds Clouds { get; set; } // cloud stuff
        public Wind Wind { get; set; } 
        public int Visibility { get; set; }
        public double Pop { get; set; } // probability of precipitation
        public Rain Rain { get; set; } // rain volume
        //public Sys Sys { get; set; } // probably won't need this, can get it from the time
        public string DtTxt { get; set; }

        public override string ToString()
        {
            string retvalue;
            retvalue = "[ DT: " + Dt.ToString() + "    Main: " + Main.ToString() + "\nList of Weather: \n{";
            foreach (Weather listelement in Weather)
            {
                retvalue = retvalue + listelement.ToString() + "\n";
            }
            retvalue = retvalue + "}\n" +
                       "   Clouds: " + (Clouds?.ToString() ?? "N/A") + "\n" +
                       "Wind: " + (Wind?.ToString() ?? "N/A") + "\n" +
                       "Visibility: " + Visibility + "%\n" +
                       "Probability of precipitation: " + Pop + "%\n" +
                       "Rain: " + (Rain?.ToString() ?? "N/A") + "\n" +
                       "DtTxt: " + DtTxt + " ]\n";

            return retvalue;
        }
    }

    public class MainInfo
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        //public double TempMin { get; set; }
        //public double TempMax { get; set; }
        public int Pressure { get; set; }
        //public int SeaLevel { get; set; }
        //public int GrndLevel { get; set; }
        public int Humidity { get; set; }
        //public double TempKf { get; set; }

        public override string ToString()
        {
            string retvalue;
            retvalue = "[ Temp: " + Temp.ToString() + "K    Feels Like: " +
                FeelsLike.ToString() + "K   Pressure: " + Pressure.ToString() +
                "mmg    Humidity: " + Humidity.ToString() + "% ]";
            return retvalue;
        }
    }
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; } // "Clouds", "Clear", etc... 
        //public string Description { get; set; } // more specific, don;t need
        //public string Icon { get; set; } // inbuilt parameter for icons, no need since its too difficult
        public override string ToString()
        {
            string retvalue;
            retvalue = "[ Id: " + Id.ToString() + "Main: " + Main + " ]";
            return retvalue;
        }
    }

    public class Clouds
    {
        public int All { get; set; } // cloudiness...?

        public override string ToString()
        {
            string retvalue;
            retvalue = "[ Cloudiness: " + All.ToString() + "% ]";
            return retvalue;
        }
    }

    public class Wind
    {
        public double Speed { get; set; } // wind speed
        public int Deg { get; set; } // wind direction
        public double Gust { get; set; } // wind gust speed: up to

        public override string ToString()
        {
            string retvalue;
            retvalue = "[ Wind speed: " + Speed.ToString() + " m/s    Deg: " + Deg.ToString() + " degrees    Gust: " + Gust.ToString() + " m/s ]";
            return retvalue;
        }
    }

    public class Rain
    {
        public double H3 { get; set; } // rain chance

        public override string ToString()
        {
            string retvalue;
            retvalue = "[ Chance of precipitation: " + H3.ToString() + "% ]";
            return retvalue;
        }
    }

    /*public class Sys
    {
        public string Pod { get; set; } // unneeded lol
    } */

    public class City
    {
        /*public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
        public int Population { get; set; } */
        public int Timezone { get; set; } 
        public long Sunrise { get; set; }
        public long Sunset { get; set; }

        public override string ToString()
        {
            string retvalue;
            retvalue = "[ Timezone: " + Timezone.ToString() + "Sunrize: " + Sunrise.ToString() + "Sunset: " + Sunset.ToString() + " ]";
            return retvalue;
        }
    }

    /*public class Coord
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    } */
}
