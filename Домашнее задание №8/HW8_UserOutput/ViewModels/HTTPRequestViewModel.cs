using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ReactiveUI;
using RestSharp;
using RestSharp.Serializers.Json;
using HW8_UserOutput.Models;
namespace HW8_UserOutput.ViewModels
{
    public class HTTPRequestViewModel : ReactiveObject
    {
        private static readonly string _url = "https://jsonplaceholder.typicode.com/";
        private RestClientOptions options = new RestClientOptions(_url);
        JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        private RestClient client;

        public HTTPRequestViewModel()
        {
            this.client = new RestClient(
                options,
                configureSerialization: (s) => s.UseSystemTextJson(serializerOptions)
            );
        }

        public async Task<List<Users>?> GetUsers()
        {
            var response = await client.GetJsonAsync<List<Users>>("users");
            return response;
        }
    }
}
