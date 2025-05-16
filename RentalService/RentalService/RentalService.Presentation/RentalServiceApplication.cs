
using RentalService.Domain;
using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using RentalService.Presentation.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Media.Imaging;

namespace RentalService.Presentation
{
    public class RentalServiceApplication
    {
        private readonly DomainManager _domainManager;
        private readonly OverviewWindow _overviewWindow;
        private readonly LoginWindow _loginWindow;
        private readonly RentalServiceApplication _rentalServiceApplication;
        //private readonly IEstablishmentRepository _establishmentRepository;
        //private readonly ICarRepository _carRepository;


        public RentalServiceApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;

            _loginWindow = new LoginWindow(domainManager);
            _loginWindow.Show();
            //logica om windows te openen moet hier
            //geen referentie naar domainmanager in windows?
        }

        internal List<CarDTO> GetCars()
        {
            return _domainManager.GetCars();
        }
        internal List<EstablishmentDTO> GetEstablishments() 
        {
            return _domainManager.GetEstablishments();
        }
        internal List<CustomerDTO> GetCustomer()
        {
            return _domainManager.GetCustomers();
        }
        internal void MakeReservation(DateTime startDate, DateTime endDate, CustomerDTO customer, CarDTO car, EstablishmentDTO establishment)
        {
            //_domainManager.MakeReservation(startDate, endDate, customer, car, establishment);
        }
    }

}
