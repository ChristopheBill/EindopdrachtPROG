using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;
using System.Net.WebSockets;

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
        //public List<CarDTO> GetCarsById(int carId)
        //{
        //    return _carRepository.GetCarsById(carId).Select(c=>new CarDTO(c)).ToList();
        //}
        public List<CarDTO> GetCarsByEstablishment(int establishmentId)
        {
            return _carRepository.GetCarsByEstablishment(establishmentId).Select(c => new CarDTO(c)).ToList();
        }
        public string GetCarById(int carId)
        {
            return _carRepository.GetCarById(carId).Model;
        }

        public List<CustomerDTO> GetCustomers() 
        {
            return _customerRepository.GetCustomers().Select(c=>new CustomerDTO(c)).ToList();
        }
        public List<EstablishmentDTO> GetEstablishments() 
        {
            return _establishmentRepository.GetEstablishments().Select(e=>new EstablishmentDTO(e)).ToList();
        }
        public List<ReservationDTO> GetReservations()
        {
            return _reservationRepository.GetReservations().Select(r => new ReservationDTO(r)).ToList();
        }
        public List<ReservationDTO> GetReservationsByCustomerIdEstablishmentId(int customerId, int establishmentId)
        {
            return _reservationRepository.GetReservationsByCustomerIdEstablishmentId(customerId, establishmentId).Select(r=>new ReservationDTO(r)).ToList();
        }

        public void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId)
        {
            _reservationRepository.MakeReservation(startDate, endDate, customerId, carId, establishmentId);
        }
        //public CustomerDTO GetCustomerById(int customerId)
        //{
            //return _customerRepository.GetCustomerById(customerId).;
        //}

    }
}
