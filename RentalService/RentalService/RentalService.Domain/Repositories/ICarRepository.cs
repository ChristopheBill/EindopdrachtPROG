using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Repositories
{
    public interface ICarRepository
    {
        public List<Car> GetCars();
        //public List<Car> GetCarsById(int carId);
        public Car GetCarById(int carId);
        public List<Car> GetCarsByEstablishment(int establishmentId);
        public List<Car> GetCarsBySeatsEstablishmentAvailability(int establishmentId, int seats, DateTime start, DateTime stop);
        public void ReadCars(string pad);
        public void GenerateMarkdown(int carId, int establishmentId);

    }
}
