using RestSharp;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class GeocodingRequestHandler : IHTTPRequestHandler // http:// api.openweathermap.org/geo/1.0/direct?q={city name},{state code},{country code}&limit={limit}&appid={API key}
    {
        private readonly string _JSON_cityFileName = @".\files\geocode.json";
        private readonly string APIkey = "e7949fa583aeaced365ea1e4710ab69e";

        public async Task<string> HandleRequest(string? cityname = null, double lat = 999.9, double lon = 999.9)
        {
            if (cityname == null) { throw new ArgumentNullException(nameof(cityname)); }

            cityname = JsonNamingPolicy.CamelCase.ConvertName(cityname);

            if (!File.Exists(_JSON_cityFileName))
            {

                string returnval;

                try
                {
                    returnval = await GeocodingRequest(cityname);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return "Failed";
                }

                return returnval;

            }
            else
            {
                List<Location>? locations = null;
                try
                {
                    locations = await JSON_Handler.ReadJSONAsync<List<Location>>(_JSON_cityFileName);
                }
                catch(Exception ex) 
                { 
                    Console.WriteLine(ex.Message);
                }
                if (locations == null) throw new Exception("failed to read geocode.json in the request handler like a russian 4th grader ");
                Location location = locations[0];
                Console.WriteLine(location.ToString());
                string filecityName = JsonNamingPolicy.CamelCase.ConvertName(location.Name);
                if (filecityName == cityname)
                {
                    Console.WriteLine("No need to update");
                    return "No need to update";
                }
                else
                {
                    try
                    {
                        await GeocodingRequest(cityname);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return "Failed";
                    }
                    
                    return "New info obtained!";
                }
            }
        }

        private async Task<string> GeocodingRequest(string cityName)
        {
            Uri baseUri = new Uri("http://api.openweathermap.org/geo/1.0");
            var options = new RestClientOptions(baseUri);
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip,
            };

            var client = new RestClient(options, configureSerialization: (s) => s.UseSystemTextJson(serializerOptions));
            var request = new RestRequest("direct");
            request.AddParameter("q", cityName);
            request.AddParameter("limit", 1);
            request.AddParameter("appid", APIkey);
            var response = await client.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await JSON_Handler.WriteJSONAsync(response.Content, _JSON_cityFileName);

                List<Location>? locations = JsonSerializer.Deserialize<List<Location>>(response.Content, serializerOptions);
                Location location;
                if (locations != null && locations.Count > 0)
                {
                    location = locations[0];
                    Console.WriteLine(location.ToString());
                    Console.WriteLine(location.Name + " lat: "  + location.Lat.ToString() + " lon: " + location.Lon.ToString());
                }
                else
                {
                    throw new Exception("Fetching failed :c");
                }
            }
            return "Geocode file creation success";
        }
    }
}
