using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Domain
{
    public class DomainManager
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEstablishmentRepository _establishmentRepository;

        public DomainManager(ICarRepository carRepository, ICustomerRepository customerRepository, IEstablishmentRepository locationRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _establishmentRepository = locationRepository;
        }
        public List<CarDTO> GetCars()
        {
            return _carRepository.GetCars().Select(c=>new CarDTO(c)).ToList();
        }

        public List<CustomerDTO> GetCustomer() 
        {
            return _customerRepository.GetCustomers().Select(c=>new CustomerDTO(c)).ToList();
        }

        public List<EstablishmentDTO> GetEstablishments() 
        {
            return _establishmentRepository.GetEstablishments().Select(e=>new EstablishmentDTO(e)).ToList();
        }

        //public void ReadEstablishments(string pad)
        //{
        //    _establishmentRepository.ReadEstablishments(pad);
        //}
    }
}
