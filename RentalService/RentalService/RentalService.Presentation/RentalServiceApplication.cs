
using RentalService.Domain;
using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using RentalService.Presentation.Windows;
using System.Windows.Automation.Peers;

namespace RentalService.Presentation
{
    public class RentalServiceApplication
    {
        private readonly DomainManager _domainManager;
        private readonly OverviewWindow _overviewWindow;
        //private readonly IEstablishmentRepository _establishmentRepository;
        //private readonly ICarRepository _carRepository;


        public RentalServiceApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;

            _overviewWindow = new OverviewWindow(domainManager);
            _overviewWindow.Show();
        }

        internal List<CarDTO> GetCars()
        {
            return _domainManager.GetCars();
        }
        internal List<EstablishmentDTO> GetEstablishments() 
        {
            return _domainManager.GetEstablishments();
        }
    }

}
