using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using ClassTypeThing.Controls;
using ClassTypeThing.Models;
using ClassTypeThing.Views;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reactive;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClassTypeThing.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        private object? _cnt;
        public object? ContentVM
        {
            get => _cnt;
            set
            {
                Console.WriteLine("changed contentvm: " + value);
                this.RaiseAndSetIfChanged(ref _cnt, value);
            }
        }
        private int _cntr = 1;
        public MainWindowViewModel() 
        {
            var user = new Users
            {
                address = new Address { City = "moscow", Geo = new Geo() },
                name = "Name",
                phone = "1234567890",
                company = new Company()
            };

            var car = new Car
            {
                Id = 1,
                Details = new CarDetails
                {
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2020
                }
            };

            var animal = new Animal
            {
                Id = 1,
                Info = new AnimalInfo
                {
                    Species = "Dog",
                    Name = "Buddy",
                    Age = 5
                }
            };

            var book = new Book
            {
                Id = 1,
                Details = new BookDetails
                {
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Pages = 180
                }
            };

            var movie = new Movie
            {
                Id = 1,
                Details = new MovieDetails
                {
                    Title = "Inception",
                    Director = "Christopher Nolan",
                    ReleaseYear = 2010
                }
            };

            var computer = new Computer
            {
                Id = 1,
                Details = new ComputerDetails
                {
                    Brand = "Apple",
                    Model = "MacBook Pro",
                    Processor = "Intel Core i7",
                    RAM = 16
                }
            };

            ContentVM = user;
            // Create a list of 10 different complex objects
            List<object> complexObjects = new List<object>
            {
                user,                                          // user
                new HttpClient(),                               // HttpClient
                car,
                new FileInfo("example.txt"),                    // FileInfo
                animal,
                new StringBuilder("Hello, world!"),             // StringBuilder
                new DataSet(),                                  // DataSet
                book,
                new RSACryptoServiceProvider(),                 // RSACryptoServiceProvider
                new Dictionary<string, int>(),                  // Dictionary<TKey, TValue>
                Task.FromResult(42),                            // Task<int>
                movie,
                new XmlDocument(),                              // XmlDocument
                new SKBitmap(100, 100),                         // Bitmap
                computer,
                Process.GetCurrentProcess(),                     // Process
                2,
                "52",
                false,
                0.2,
                Convert.ToByte(5),
                Convert.ToDateTime(DateTime.Now),
                Convert.ToDecimal(0),
                '!'

            };

            Incr = ReactiveCommand.Create(() =>
            {
                _cntr %= complexObjects.Count;
                ContentVM = complexObjects[_cntr];
                _cntr++;
            });
            
            
        }
        public ReactiveCommand<Unit, Unit> Incr { get; }
    }
}
