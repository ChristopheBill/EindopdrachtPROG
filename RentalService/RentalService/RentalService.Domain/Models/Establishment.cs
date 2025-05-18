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

                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual((value), 0);
                _id = value;
            }
        }
        public string Airport 
        { 
            get => _airport; 
            init 
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Airport));
                _airport = value; 
            }
        }
        public string Street
        {
            get => _street;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Street));
                _street = value;
            }
        }
        public string PostalCode
        {
            get => _postalCode; 
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(PostalCode));
                _postalCode = value; 
            }
        }
        public string City 
        { get => _city;
            init
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(City));
                _city = value; 
            }
        }
        public string Country
        {
            get => _country;
            init //logica om land te checken op cijfers in de naam
            => _country = value;
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
