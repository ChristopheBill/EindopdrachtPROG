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
    public class ReservationDTO
    {
        //private ICarRepository _carRepository;
        public ReservationDTO(Reservation reservation) 
        {
            Id = reservation.Id;
            StartDatum = reservation.StartDatum;
            EindDatum = reservation.EindDatum;
            CustomerId = reservation.CustomerId;
            CarId = reservation.CarId;
            EstablishmentId = reservation.EstablishmentId;
        }

        public int Id { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public int CustomerId { get; set; }
        public int CarId{ get; set; }
        public int EstablishmentId { get; set; }

        //public override string? ToString()
        //{
        //    ICarRepository carRepository = _carRepository;
        //    return $"{carRepository.GetCarById(CarId).Model}";
        //}
    }
}
