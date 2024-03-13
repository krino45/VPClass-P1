using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class MakeRequest
    {
        public static bool workflag { get; set; } = false;
        public static async Task Make_a_Request(string result) 
        {
            workflag = true;
            IHTTPRequestHandler p = await RequestHandlerFactory.CreateHandler(cityname: result);

            if (p is ForecastRequestHandler)
            {
                List<Location>? location = await JSON_Handler.ReadJSONAsync<List<Location>>(@".\files\geocode.json");

                if (location == null)
                {
                    throw new Exception("couldnt read geocode from codebehind of main window ");
                }

                p = await RequestHandlerFactory.CreateHandler();

                Console.WriteLine("here are lat and lon");
                Console.WriteLine(location[0].Lat + " " + location[0].Lon);

                await p.HandleRequest(lat: location[0].Lat, lon: location[0].Lon);

            }
            else if (p is GeocodingRequestHandler)
            {
                string returnstr = await p.HandleRequest(cityname: result);
                if (returnstr.Equals("Bad fetch")) { Console.WriteLine("hiiii youre about to fail!"); await p.HandleRequest("Novosibirsk"); workflag = false; return; }
                List<Location?> location = new List<Location?>();
                try
                {
                    location = await JSON_Handler.ReadJSONAsync<List<Location?>>(@".\files\geocode.json");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                    await Make_a_Request("Novosibirsk");
                }
                if (location == null)
                {
                    throw new Exception("couldnt read geocode from codebehind of main window ");
                }
                if (location.Count == 0)
                {
                    Console.WriteLine($"Could not find location {result}");
                    p = await RequestHandlerFactory.CreateHandler("Novosibirsk");
                    await p.HandleRequest("Novosibirsk");
                    await Make_a_Request("Novosibirsk");
                    workflag = false;
                    return;
                }

                p = await RequestHandlerFactory.CreateHandler();

                Console.WriteLine("here are lat and lon");
                Console.WriteLine(location[0].Lat + " " + location[0].Lon);

                await p.HandleRequest(lat: location[0].Lat, lon: location[0].Lon);

            }
            else { throw new Exception("Nobody knows who the p Handler is... "); }
            workflag = false;
        }
        public static async Task Make_a_Request_Geocode()
        {
            workflag = true;
            if (!File.Exists(@"./files/geocode.json")) { workflag = false; throw new Exception("Wrong use of Make_a_Request_Geocode"); }

            IHTTPRequestHandler p = await RequestHandlerFactory.CreateHandler();

            if (p is ForecastRequestHandler)
            {
                List<Location>? location = await JSON_Handler.ReadJSONAsync<List<Location>>(@".\files\geocode.json");

                if (location == null)
                {
                    throw new Exception("couldnt read geocode from codebehind of main window ");
                }

                p = await RequestHandlerFactory.CreateHandler();

                Console.WriteLine("here are lat and lon");
                Console.WriteLine(location[0].Lat + " " + location[0].Lon);

                await p.HandleRequest(lat: location[0].Lat, lon: location[0].Lon);

            }
            else { workflag = false; throw new Exception("Wrong use of Make_a_Request_Geocode"); }
            workflag = false;
        }
    }
}
