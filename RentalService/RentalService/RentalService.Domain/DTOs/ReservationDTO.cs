using RentalService.Domain.Models;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.DTOs
{
    public record ReservationDTO
    {
        public ReservationDTO()
        {
        }

        public ReservationDTO(int id, DateTime startDate, DateTime endDate, CustomerDTO customer, CarDTO car, EstablishmentDTO establishment) 
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Customer = customer;
            Car = car;
            Establishment = establishment;
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public CarDTO Car { get; set; }
        public EstablishmentDTO Establishment { get; set; }

    }
}
