using RentalService.Domain.DTOs;
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
        //Car related methods
        public List<CarDTO> GetCars()
        {
            return _carRepository.GetCars().Select(c=>new CarDTO(c.Id, c.LicensePlate, c.Model, c.Seats, c.MotorType, c.Establishment)).ToList();
        }
        public List<CarDTO> GetCarsByEstablishment(int establishmentId)
        {
            return _carRepository.GetCarsByEstablishment(establishmentId).Select(c => new CarDTO(c.Id, c.LicensePlate, c.Model, c.Seats, c.MotorType, c.Establishment)).ToList();
        }
        public List<CarDTO> GetCarsBySeatsEstablishmentAvailability(int establishmentId, int seats, DateTime start, DateTime stop)
        {
            return _carRepository.GetCarsBySeatsEstablishmentAvailability(establishmentId, seats, start, stop).Select(c => new CarDTO(c.Id, c.LicensePlate, c.Model, c.Seats, c.MotorType, c.Establishment)).ToList();
        }
        public void ReadCars(string pad)
        {
            _carRepository.ReadCars(pad);
        }
        public void GenerateMarkdown(int carId, int establishmentId)
        {
            _carRepository.GenerateMarkdown(carId, establishmentId);
        }

        //Customer related methods
        public List<CustomerDTO> GetCustomers() 
        {
            return _customerRepository.GetCustomers().Select(c=>new CustomerDTO(c.Id, c.FirstName, c.LastName, c.Email, c.Street, c.PostalCode, c.City, c.Country)).ToList();
        }
        public void ReadCustomers(string pad)
        {
            _customerRepository.ReadCustomers(pad);
        }

        //Establishment related methods
        public List<EstablishmentDTO> GetEstablishments() 
        {
            return _establishmentRepository.GetEstablishments().Select(e=>new EstablishmentDTO(e.Id, e.Airport, e.Street, e.PostalCode, e.City, e.Country)).ToList();
        }
        public void ReadEstablishments(string pad)
        {
            _establishmentRepository.ReadEstablishments(pad);
        }

        //Reservation related methods
        public List<ReservationDTO> GetReservations()
        {
            return _reservationRepository.GetReservations().Select(r => new ReservationDTO(r.Id, r.StartDate, r.EndDate, r.Customer, r.Car, r.Establishment)).ToList();
        }
        public List<ReservationDTO> GetReservationsByCustomerIdEstablishmentIdDate(int customerId, int establishmentId, DateTime date)
        {
            return _reservationRepository.GetReservationsByCustomerIdEstablishmentIdDate(customerId, establishmentId, date).Select(r=>new ReservationDTO(r.Id, r.StartDate, r.EndDate, r.Customer, r.Car, r.Establishment)).ToList();
        }
        public void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId)
        {
            _reservationRepository.MakeReservation(startDate, endDate, customerId, carId, establishmentId);
        }
        public void RemoveReservation(int reservationId)
        {
            _reservationRepository.RemoveReservation(reservationId);
        }
    }
}
