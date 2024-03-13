using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal static class JSON_Handler
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public static async Task WriteJSONAsync(object obj, string fileName)
        {
            var jsonString = JsonSerializer.Serialize(obj, _options);
            jsonString = jsonString.Replace("\\n", "\n");
            jsonString = jsonString.Replace("\\\n", "\\n");
            jsonString = jsonString.Replace("\\t", "\t");
            jsonString = jsonString.Replace("\\\\", "\\");
            //jsonString = jsonString.Replace("\\u0022", "\u0022");
            jsonString = jsonString.Trim('"');
            jsonString = Regex.Unescape(jsonString);
            await File.WriteAllTextAsync(fileName, jsonString);
        }

        public static async Task<T?> ReadJSONAsync<T>(string filename)
        {
            var jsonString = await File.ReadAllTextAsync(filename);
            return JsonSerializer.Deserialize<T>(jsonString, _options);
        }


    }
}