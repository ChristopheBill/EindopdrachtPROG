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
    }
}
