using RentalService.Domain.Repositories;

namespace RentalService.Domain
{
    public class DomainManager
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILocationRepository _locationRepository;

        public DomainManager(ICarRepository carRepository, ICustomerRepository customerRepository, ILocationRepository locationRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _locationRepository = locationRepository;
        }
    }
}
