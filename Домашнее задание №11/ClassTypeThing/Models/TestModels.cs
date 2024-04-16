namespace ClassTypeThing.Models
{
    public class Geo
    {
        private string? lat;
        private string? lng;

        public string? Lat { get => lat; set => lat = value; }
        public string? Lng { get => lng; set => lng = value; }
    }

    public class Company
    {
        private string? name;
        private string? catchPhrase;
        private string? bs;

        public string? Name { get => name; set => name = value; }
        public string? CatchPhrase { get => catchPhrase; set => catchPhrase = value; }
        public string? Bs { get => bs; set => bs = value; }
    }

    public class Address
    {
        private string? street;
        private string? suite;
        private string? city;
        private string? zipcode;
        private Geo? geo;

        public string? Street { get => street; set => street = value; }
        public string? Suite { get => suite; set => suite = value; }
        public string? City { get => city; set => city = value; }
        public string? Zipcode { get => zipcode; set => zipcode = value; }
        public Geo? Geo { get => geo; set => geo = value; }
    }
    public class Users
    {
        private int id;

        private string? name;

        private string? username;

        private string? email;

        private Address? address;

        private string? phone;

        private string? website;

        private Company? company;

        public int Id { get => id; set => id = value; }
        public string? Name { get => name; set => name = value; }
        public string? Username { get => username; set => username = value; }
        public string? Email { get => email; set => email = value; }
        public Address? Address { get => address; set => address = value; }
        public string? Phone { get => phone; set => phone = value; }
        public string? Website { get => website ; set => website = value; }
        public Company? Company { get => company;  set => company = value; }
    }
    public class Animal
    {
        private int _id;
        private AnimalInfo _info;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public AnimalInfo Info
        {
            get => _info;
            set => _info = value;
        }
    }

    public class AnimalInfo
    {
        private string? _species;
        private string? _name;
        private int _age;

        public string? Species
        {
            get => _species;
            set => _species = value;
        }

        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }
    }

    public class Book
    {
        private int _id;
        private BookDetails _details;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public BookDetails Details
        {
            get => _details;
            set => _details = value;
        }
    }

    public class BookDetails
    {
        private string? _title;
        private string? _author;
        private int _pages;

        public string? Title
        {
            get => _title;
            set => _title = value;
        }

        public string? Author
        {
            get => _author;
            set => _author = value;
        }

        public int Pages
        {
            get => _pages;
            set => _pages = value;
        }
    }

    public class Movie
    {
        private int _id;
        private MovieDetails _details;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public MovieDetails Details
        {
            get => _details;
            set => _details = value;
        }
    }

    public class MovieDetails
    {
        private string? _title;
        private string? _director;
        private int _releaseYear;

        public string? Title
        {
            get => _title;
            set => _title = value;
        }

        public string? Director
        {
            get => _director;
            set => _director = value;
        }

        public int ReleaseYear
        {
            get => _releaseYear;
            set => _releaseYear = value;
        }
    }

    public class Computer
    {
        private int _id;
        private ComputerDetails _details;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public ComputerDetails Details
        {
            get => _details;
            set => _details = value;
        }
    }

    public class ComputerDetails
    {
        private string? _brand;
        private string? _model;
        private string? _processor;
        private int _ram;

        public string? Brand
        {
            get => _brand;
            set => _brand = value;
        }

        public string? Model
        {
            get => _model;
            set => _model = value;
        }

        public string? Processor
        {
            get => _processor;
            set => _processor = value;
        }

        public int RAM
        {
            get => _ram;
            set => _ram = value;
        }
    }
        public class Car
        {
            private int _id;
            private CarDetails _details;

            public int Id
            {
                get => _id;
                set => _id = value;
            }

            public CarDetails Details
            {
                get => _details;
                set => _details = value;
            }
        }

    public class CarDetails
    {
        private string? _make;
        private string? _model;
        private int _year;

        public string? Make
        {
            get => _make;
            set => _make = value;
        }

        public string? Model
        {
            get => _model;
            set => _model = value;
        }

        public int Year
        {
            get => _year;
            set => _year = value;
        }
    }
}
