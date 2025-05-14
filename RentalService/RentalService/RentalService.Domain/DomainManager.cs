using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Domain
{
    public class DomainManager
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEstablishmentRepository _locationRepository;

        public DomainManager(ICarRepository carRepository, ICustomerRepository customerRepository, IEstablishmentRepository locationRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _locationRepository = locationRepository;
        }
        public List<CarDTO> GetCars()
        {
            return _carRepository.GetCars().Select(c=>new CarDTO(c)).ToList();
        }
    }
}
