using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.DTOs
{
    public record CustomerDTO
    {
        public CustomerDTO(int id, string firstName, string lastName, string email, string street, string postalCode, string city, string country)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Street = street;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override string? ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
