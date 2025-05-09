using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Models
{
    public class Establishment
    {
        public Establishment(string airport, string streetName, int postalCode, int city, string country)
        {
            Airport = airport;
            StreetName = streetName;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public string Airport { get; init; }
        public string StreetName { get; init; }
        public int PostalCode { get; init; }
        public int City { get; init; }
        public string Country 
        {
            get; 
            init //logica om land te checken op cijfers in de naam
                ;
        }
    }
}
