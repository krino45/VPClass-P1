using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WeatherApp
{
    public class RequestHandlerFactory
    {
        // Will return Geocoding request handler if the city name isn't specified and the geocode file exists, if the specified name differs, or if the geocode file doesn't exist. Returns weather handler in the other cases.
        public static async Task<IHTTPRequestHandler> CreateHandler(string? cityname = null)
        {
            if(cityname == null)
            {
                if (!File.Exists(@".\files\geocode.json"))
                {
                    return new GeocodingRequestHandler();
                }
                else
                {
                    return new ForecastRequestHandler();
                }
            }
            else
            {
                if (File.Exists(@".\files\geocode.json"))
                {
                    List<Location>? locations = await JSON_Handler.ReadJSONAsync<List<Location>>(@".\files\geocode.json");
                    if (locations == null)
                    {
                        throw new System.Exception("Couldn't open file in: RequestHandlerFactory. ");
                    }
                    string filecityName;
                    try
                    {
                        filecityName = JsonNamingPolicy.CamelCase.ConvertName(locations[0].Name);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        filecityName = "Novosibirsk";
                    }
                    if (filecityName == cityname) 
                    {
                        return new ForecastRequestHandler();
                    }
                    else
                    {
                        return new GeocodingRequestHandler();
                    }
                }
                else
                {
                    return new GeocodingRequestHandler();
                }

            }
        }

    }
}
