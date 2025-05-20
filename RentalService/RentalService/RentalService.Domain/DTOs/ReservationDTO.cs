using RentalService.Domain.Models;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.DTOs
{
    public record ReservationDTO
    {
        //private ICarRepository _carRepository;
        public ReservationDTO(Reservation reservation) 
        {
            Id = reservation.Id;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
            Customer = reservation.Customer;
            Car = reservation.Car;
            Establishment = reservation.Establishment;
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public Establishment Establishment { get; set; }

        //public override string? ToString()
        //{
        //    ICarRepository carRepository = _carRepository;
        //    return $"{carRepository.GetCarById(CarId).Model}";
        //}
    }
}
