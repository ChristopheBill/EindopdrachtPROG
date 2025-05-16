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
        private readonly IReservationRepository _reservationRepository;

        public DomainManager(ICarRepository carRepository, ICustomerRepository customerRepository, IEstablishmentRepository locationRepository, IReservationRepository reservationRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _establishmentRepository = locationRepository;
            _reservationRepository = reservationRepository;
        }
        public List<CarDTO> GetCars()
        {
            return _carRepository.GetCars().Select(c=>new CarDTO(c)).ToList();
        }

        public List<CustomerDTO> GetCustomers() 
        {
            return _customerRepository.GetCustomers().Select(c=>new CustomerDTO(c)).ToList();
        }
        //public CustomerDTO GetCustomerById(int customerId)
        //{
            //return _customerRepository.GetCustomerById(customerId).;
        //}

        public List<EstablishmentDTO> GetEstablishments() 
        {
            return _establishmentRepository.GetEstablishments().Select(e=>new EstablishmentDTO(e)).ToList();
        }
        //public List<EstablishmentDTO> GetReservations()
        //{
        //    return _reservationRepository.GetReservations().Select(r => new EstablishmentDTO(r)).ToList();
        //}

        //public void ReadEstablishments(string pad)
        //{
        //    _establishmentRepository.ReadEstablishments(pad);
        //}
        public void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId)
        {
            _reservationRepository.MakeReservation(startDate, endDate, customerId, carId, establishmentId);
        }

        //public void MakeReservation(DateTime startDate, DateTime endDate, CustomerDTO customer, CarDTO car, EstablishmentDTO establishment)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
