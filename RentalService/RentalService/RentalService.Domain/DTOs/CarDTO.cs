using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.DTOs
{
    public class CarDTO
    {
        public CarDTO(Car car) 
        {
            Id = car.Id;
            LicensePlate = car.LicensePlate;
            Model = car.Model;
            Seats = car.Seats;
            MotorType = car.MotorType;
            Establishment = car.Establishment;
        }
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public int Seats { get; set; }
        public string MotorType { get; set; }
        public Establishment Establishment { get; set; }

    }
}
