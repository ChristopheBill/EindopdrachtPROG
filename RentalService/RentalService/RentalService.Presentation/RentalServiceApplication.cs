
using RentalService.Domain;
using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;
using RentalService.Presentation.Windows;
using System.Collections;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Media.Imaging;

namespace RentalService.Presentation
{
    public class RentalServiceApplication
    {
        private readonly DomainManager _domainManager;
        private readonly OverviewWindow _overviewWindow;
        private readonly LoginWindow _loginWindow;
        private readonly CarOverviewWindow _carOverviewWindow;
        private readonly ReservationSearchWindow _reservationSearchWindow;
        private readonly ReservationCreateWindow _reservationCreateWindow;
        private readonly RentalServiceApplication _rentalServiceApplication;
        private CustomerDTO _customer;

        public RentalServiceApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;
            _loginWindow = new LoginWindow(this);
            _loginWindow.Show();

            //logica om windows te openen moet hier
            //geen referentie naar domainmanager in windows?
        }
        internal void TakeToOverviewWindow(Window window, CustomerDTO customer)
        {
            _customer = customer;
            new OverviewWindow(this, customer).Show();
            _loginWindow.Close();
        }
        internal void TakeToReservationCreateWindow(Window window, CustomerDTO customer)
        {
            _customer = customer;
            new ReservationCreateWindow(this, customer).Show();
        }
        internal void TakeToCarOverviewWindow(Window window)
        {
            new CarOverviewWindow(this).Show();
        }
        internal void TakeToReservationSearchWindow(Window window)
        {
            new ReservationSearchWindow(this).Show();
        }
        internal List<CarDTO> GetCars()
        {
            return _domainManager.GetCars();
        }
        internal List<CarDTO> GetCarsByEstablishment(int establishmentId)
        {
            return _domainManager.GetCarsByEstablishment(establishmentId);
        }
        internal List<CarDTO> GetCarsBySeatsEstablishmentAvailability(int establishmentId, int seats, DateTime start, DateTime stop)
        {
            return _domainManager.GetCarsBySeatsEstablishmentAvailability(establishmentId, seats, start, stop);
        }
        internal List<EstablishmentDTO> GetEstablishments() 
        {
            return _domainManager.GetEstablishments();
        }
        internal List<CustomerDTO> GetCustomers()
        {
            return _domainManager.GetCustomers();
        }
        internal List<ReservationDTO> GetReservations()
        {
            return _domainManager.GetReservations();
        }
        internal List<ReservationDTO> GetReservationsByCustomerIdEstablishmentId(int customerId, int establishmentId)
        {
            return _domainManager.GetReservationsByCustomerIdEstablishmentId(customerId, establishmentId);
        }
        internal void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId)
        {
            _domainManager.MakeReservation(startDate, endDate, customerId, carId, establishmentId);
        }
        internal void RemoveReservation(int reservationId)
        {
            _domainManager.RemoveReservation(reservationId);
        }
    }

}
