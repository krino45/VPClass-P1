using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherApp
{
    public class Location
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("local_names")]
        public Dictionary<string, string>? LocalNames { get; set; }
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
        [JsonPropertyName("lon")]
        public double Lon { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Latitude: {Lat}, Longitude: {Lon}, Country: {Country}, State: {State}";
        }
    }

}

