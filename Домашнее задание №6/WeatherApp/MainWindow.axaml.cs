using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Test(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.IsEnabled = false;
                await Task.Run(() => DialogWindow(button, null));
                button.IsEnabled = true;
            }
        }
        private async void DialogWindow(object? sender, TappedEventArgs? e)
        {
            var dialog = new DialogWindow();
            string? result = await dialog.ShowAsync();
            if (result != null)
            {
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
                    await p.HandleRequest(cityname: result);

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
                else { throw new Exception("Nobody knows who the p Handler is... "); }
            }
        }
    }
}