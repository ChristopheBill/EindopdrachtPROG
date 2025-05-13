using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.DTOs
{
    public class EstablishmentDTO
    {
        public EstablishmentDTO(Establishment establishment)
        {
            Id = establishment.Id;
            Airport = establishment.Airport;
            Street = establishment.Street;
            PostalCode = establishment.PostalCode;
            City = establishment.City;
            Country = establishment.Country;
        }

        public int Id { get; set; }
        public string Airport { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
