using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public interface IHTTPRequestHandler
    {
        Task<string> HandleRequest(string? cityname = null, double lat = 999.9, double lon = 999.9);
    }

}