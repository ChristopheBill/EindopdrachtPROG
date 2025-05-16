
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
        //private readonly IEstablishmentRepository _establishmentRepository;
        //private readonly ICarRepository _carRepository;
        private string _loggedInCustomer;


        public RentalServiceApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;

            _loginWindow = new LoginWindow(this);
            _carOverviewWindow = new CarOverviewWindow(this);
            _reservationCreateWindow = new ReservationCreateWindow(this);
            _reservationCreateWindow = new ReservationCreateWindow(this);
            _reservationSearchWindow = new ReservationSearchWindow(this);
            _loginWindow.Show();

            //logica om windows te openen moet hier
            //geen referentie naar domainmanager in windows?
        }
        internal void TakeToOverviewWindow(Window window, string customerName)
        {
            _loginWindow.Close();
            _loggedInCustomer = customerName;
            new OverviewWindow(this, customerName).Show();
        }
        internal void TakeToReservationCreateWindow(Window window)
        {
            window.Close();
            _reservationCreateWindow.Show();
        }
        internal void TakeToCarOverviewWindow(Window window)
        {
            window.Close();
            _loginWindow.Show();
        }
        internal void TakeToReservationSearchWindow(Window window)
        {
            window.Close();
            _reservationSearchWindow.Show();
        }

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
        internal void MakeReservation(DateTime startDate, DateTime endDate, CustomerDTO customer, CarDTO car, EstablishmentDTO establishment)
        {
            //_domainManager.MakeReservation(startDate, endDate, customer, car, establishment);
        }
    }

}
