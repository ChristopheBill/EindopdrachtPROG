
using RentalService.Domain;
using RentalService.Domain.DTOs;
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
            //_carOverviewWindow = new CarOverviewWindow(this);
            //_reservationCreateWindow = new ReservationCreateWindow(this);
            //_reservationCreateWindow = new ReservationCreateWindow(this);
            //_reservationSearchWindow = new ReservationSearchWindow(this);
            _loginWindow.Show();

            //logica om windows te openen moet hier
            //geen referentie naar domainmanager in windows?
        }
        internal void TakeToOverviewWindow(Window window, CustomerDTO customer)
        {
            _customer = customer;
            new OverviewWindow(this, customer).Show();
            _loginWindow.Close();
            //CustomerDTO customer = _domainManager.GetCustomerById(customerId);
        }
        internal void TakeToReservationCreateWindow(Window window, CustomerDTO customer)
        {
            _customer = customer;
            new ReservationCreateWindow(this, customer).Show();
            window.Close();
            //_reservationCreateWindow.Show();
        }
        internal void TakeToCarOverviewWindow(Window window)
        {
            new CarOverviewWindow(this).Show();
            window.Close();
            //_loginWindow.Show();
        }
        internal void TakeToReservationSearchWindow(Window window)
        {
            new ReservationSearchWindow(this).Show();
            window.Close();
            //_reservationSearchWindow.Show();
        }
        //internal CustomerDTO GetCustomerById(int customerId)
        //{
        //    return _domainManager.GetCustomerById(customerId);
        //}

        public string GetCustomerName(CustomerDTO customer)
        {
            return $"{customer.FirstName} {customer.LastName}";
        }
        internal List<CarDTO> GetCars()
        {
            return _domainManager.GetCars();
        }
        internal List<EstablishmentDTO> GetEstablishments() 
        {
            return _domainManager.GetEstablishments();
        }
        internal List<CustomerDTO> GetCustomers()
        {
            return _domainManager.GetCustomers();
        }
        //internal List<ReservationDTO> GetReservations()
        //{
        //    //return _domainManager.GetReservations();
        //}
        internal void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId)
        {
            _domainManager.MakeReservation(startDate, endDate, customerId, carId, establishmentId);
        }
    }

}
