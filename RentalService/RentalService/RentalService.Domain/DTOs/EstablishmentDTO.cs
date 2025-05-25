using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.DTOs
{
    public record EstablishmentDTO
    {
        public EstablishmentDTO(int id, string airport, string street, string postalCode, string city, string country)
        {
            Id = id;
            Airport = airport;
            Street = street;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public int Id { get; set; }
        public string Airport { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override string? ToString()
        {
            return $"{Airport} {City}";
        }
    }
}
