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
        public string Airport { get; init; }
        public string Street { get; init; }
        public string PostalCode { get; init; }
        public string City { get; init; }
        public string Country 
        {
            get; 
            init //logica om land te checken op cijfers in de naam
                ;
        }
    }
}
