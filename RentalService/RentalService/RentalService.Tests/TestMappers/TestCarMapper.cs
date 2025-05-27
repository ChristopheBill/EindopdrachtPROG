using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Tests.TestMappers
{
    class TestCarMapper : ICarRepository
    {
        public void GenerateMarkdown(int carId, int establishmentId)
        {
            throw new NotImplementedException();
        }

        public CarDTO GetCarById(int carId)
        {
            throw new NotImplementedException();
        }

        public List<CarDTO> GetCars()
        {
            List<CarDTO> cars = new();
            EstablishmentDTO establishment = new EstablishmentDTO(1, "TestEstablishment", "123 Test St", "TestCode", "TestCity", "TestCountry");

            cars.Add(new CarDTO(1, "BE-HDE-010", "Mercedes", 5, "Diesel", establishment));
            cars.Add(new CarDTO(2, "BE-OGN-454", "BMW", 4, "Petrol", establishment));
            cars.Add(new CarDTO(3, "BE-HOW-101", "Volvo", 2, "Electric", establishment));
            cars.Add(new CarDTO(4, "BE-WWW-100", "Bugatti", 7, "Hybrid", establishment));
            cars.Add(new CarDTO(5, "BE-ZZZ-300", "Chevrolet", 5, "Diesel", establishment));
            return cars;
        }

        public List<CarDTO> GetCarsByEstablishment(int establishmentId)
        {
            List<CarDTO> cars = GetCars();
            return cars.Where(c => c.Establishment.Id == establishmentId).ToList();
        }

        public List<CarDTO> GetCarsBySeatsEstablishmentAvailability(int establishmentId, int seats, DateTime start, DateTime stop)
        {
            throw new NotImplementedException();
        }

        public void ReadCars(string pad)
        {
            throw new NotImplementedException();
        }
    }
}
