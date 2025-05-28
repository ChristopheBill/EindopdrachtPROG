using RentalService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Repositories
{
    public interface ICarRepository
    {
        public List<CarDTO> GetCars();
        public List<CarDTO> GetCarsByEstablishment(int establishmentId);
        public List<CarDTO> GetCarsBySeatsEstablishmentAvailability(int establishmentId, int seats, DateTime start, DateTime stop);
        public void ReadCars(string pad);
        public void GenerateMarkdown(int carId, int establishmentId);

    }
}
