using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Models
{
    public class Establishment
    {
        private int _id;
        private string _airport;
        private string _street;
        private string _postalCode;
        private string _city;
        private string _country;

        public Establishment()
        {
        }

        public Establishment(string airport, string streetName, string postalCode, string city, string country)
        {
            Airport = airport;
            Street = streetName;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public Establishment(int id, string airport, string streetName, string postalCode, string city, string country) : this (airport, streetName, postalCode, city, country)
        {
            Id = id;
        }

        public int Id
        {
            get => _id;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id cannot be less than 0", (nameof(Id)));
                }
                _id = value;
            }
        }
        public string Airport 
        { 
            get => _airport; 
            init 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Airport cannot be null or empty", nameof(Airport));
                }
                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Airport cannot contain numbers", nameof(Airport));
                }
                _airport = value; 
            }
        }
        public string Street
        {
            get => _street;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Street cannot be null or empty", nameof(Street));
                }
                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Street cannot contain numbers", nameof(Street));
                }
                _street = value;
            }
        }
        public string PostalCode
        {
            get => _postalCode; 
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("PostalCode cannot be null or empty", nameof(PostalCode));
                }
                _postalCode = value; 
            }
        }
        public string City 
        { get => _city;
            init
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("City cannot be null or empty", nameof(City));
                }
                _city = value; 
            }
        }
        public string Country
        {
            get => _country;
            init {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Country cannot be null or empty", nameof(Country));
                }
                if (value.Any(char.IsDigit))
                {
                    throw new ArgumentException("Country cannot contain numbers", nameof(Country));
                }
             _country = value;
            }
        }
        public override bool Equals(object? obj)
        {
            return obj is Establishment establishment &&
                   Id == establishment.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string? ToString()
        {
            return $"{Country}";
        }
    }
}
