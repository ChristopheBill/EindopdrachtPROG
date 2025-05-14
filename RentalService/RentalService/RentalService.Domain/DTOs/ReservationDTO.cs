using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.DTOs
{
    public class ReservationDTO
    {
        public ReservationDTO(Reservation reservation) 
        {
            Id = reservation.Id;
            StartDatum = reservation.StartDatum;
            EindDatum = reservation.EindDatum;
            Customer = reservation.Customer;
            Car = reservation.Car;
            Establishment = reservation.Establishment;
        }

        public int Id { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Establishment Establishment { get; set; }
        
    }
}
