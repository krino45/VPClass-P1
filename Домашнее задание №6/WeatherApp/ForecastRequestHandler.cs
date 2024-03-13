using RestSharp;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class ForecastRequestHandler : IHTTPRequestHandler
    {
        private readonly string _JSON_weatherFileName = @".\files\forecast.json";
        private readonly string APIkey = "e7949fa583aeaced365ea1e4710ab69e";
        private long _unixSeconds;
        private string _lastReqTime = @".\bin\reqtime";
        static private readonly long _requestFrequency = (60 * 60 * 3);

        public ForecastRequestHandler()
        {
            if (File.Exists(_JSON_weatherFileName))
            {
                _unixSeconds = DateTimeOffset.FromFileTime(File.GetLastWriteTime(_JSON_weatherFileName).ToFileTime()).ToUnixTimeSeconds();
                Console.WriteLine($"_unixSeconds filetime read: {_unixSeconds}");
                Console.WriteLine($"rn: {DateTimeOffset.Now.ToUnixTimeSeconds()}");
            }
            else
            {
                _unixSeconds = 0;
            }
        }

        public async Task<string> HandleRequest(string? cityname = null, double lat = 999.9, double lon = 999.9)
        {
            if (lat != 999.9 && lon != 999.9)
            {

                string returnval;
                try
                {
                    returnval = await ForecastRequest((double)lat, (double)lon);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in HandleRequest, ForecastRequestHandler: {ex.Message}, data: {ex.Data}, source: {ex.Source} ,\nstacktrace: {ex.StackTrace}\n");
                    return "Failed";
                }
                return returnval;
            }
            else
            {
                throw new Exception("latitude and longitude not provided to forecastrequesthandler. ");
            }
        }


        public async Task<string> ForecastRequest(double lat, double lon) //api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={API key}
        {
            Console.WriteLine($"_unixSeconds: {_unixSeconds}");
            if ((DateTimeOffset.Now.ToUnixTimeSeconds() - _unixSeconds > _requestFrequency) || DateTimeOffset.Parse(File.GetLastWriteTime(@".\files\geocode.json").ToString()).ToUnixTimeSeconds() > _unixSeconds)
            {
                Console.WriteLine("Sending a forecast request...");
                Uri baseUri = new Uri("http://api.openweathermap.org/data/2.5");
                var options = new RestClientOptions(baseUri);
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip,
                };

                var client = new RestClient(options, configureSerialization: (s) => s.UseSystemTextJson(serializerOptions));
                var request = new RestRequest("forecast");
                request.AddParameter("lat", lat);
                request.AddParameter("lon", lon);
                request.AddParameter("appid", APIkey);
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await JSON_Handler.WriteJSONAsync(response.Content, _JSON_weatherFileName);
                    Forecast? forecast = JsonSerializer.Deserialize<Forecast>(response.Content, serializerOptions);
                    if (forecast == null)
                    {
                        throw new Exception("Fetching failed :c");
                    }
                    else
                    {
                        //Console.WriteLine(forecast.ToString());7
                    }
                }
                Console.WriteLine("Forecast file creation success");
                return "Forecast file creation success";
            }
            else
            {
                Console.WriteLine("No need for sending a Forecast request...");
                Console.WriteLine($"_unixSeconds: {_unixSeconds}");
                Forecast? forecast = await JSON_Handler.ReadJSONAsync<Forecast>(_JSON_weatherFileName);
                //Console.WriteLine(forecast.ToString());
                return $"Try again in {_requestFrequency - (DateTimeOffset.Now.ToUnixTimeSeconds() - _unixSeconds)} seconds";
            }
        }
    } 
}


/*
        public async Task<string> HTTPForecastRequest()
        {
            Console.WriteLine($"_unixSeconds: {_unixSeconds}");
            if (DateTimeOffset.Now.ToUnixTimeSeconds() - _unixSeconds > _requestFrequency)
            {
                Uri baseUri = new Uri("https://jsonplaceholder.typicode.com");
                var options = new RestClientOptions(baseUri);
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip,
                };

                var client = new RestClient(options, configureSerialization: (s) => s.UseSystemTextJson(serializerOptions));
                var request = new RestRequest("posts");
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await JSON_Handler.WriteJSONAsync(response.Content, _JSON_weatherFileName);
                    List<Forecast>? forecast = JsonSerializer.Deserialize<List<Forecast>>(response.Content, serializerOptions);
                    if (forecast == null)
                    {
                        throw new Exception("Fetching failed :c");
                    }
                    else
                    {
                        foreach (var lol in forecast)
                        {
                            //...
                        }
                    }
                }
                unixSecondsSetter();
                Console.WriteLine($"_unixSeconds: {_unixSeconds}");
                File.WriteAllText(_lastReqTime, _unixSeconds.ToString());
                return "File creation success";
            }
            else
            {
                Console.WriteLine($"_unixSeconds: {_unixSeconds}");
                List<Forecast>? forecast = await JSON_Handler.ReadJSONAsync<List<Forecast>>(_JSON_weatherFileName);
                foreach (var lol in forecast)
                {
                    // ...
                }
                return $"Try again in {_requestFrequency - (DateTimeOffset.Now.ToUnixTimeSeconds() - _unixSeconds)} seconds";
            }
        }
*/