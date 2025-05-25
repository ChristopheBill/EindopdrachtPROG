using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.DTOs
{
    public record CarDTO
    {
        public CarDTO()
        {
        }

        public CarDTO(int id, string licensePlate, string model, int seats, string motorType, EstablishmentDTO establishment) 
        {
            Id = id;
            LicensePlate = licensePlate;
            Model = model;
            Seats = seats;
            MotorType = motorType;
            Establishment = establishment;
        }
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public int Seats { get; set; }
        public string MotorType { get; set; }
        public EstablishmentDTO Establishment { get; set; }

    }
}
