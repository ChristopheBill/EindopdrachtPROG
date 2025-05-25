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
            throw new NotImplementedException();
        }

        public List<CarDTO> GetCarsByEstablishment(int establishmentId)
        {
            throw new NotImplementedException();
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
