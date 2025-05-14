
using RentalService.Domain;
using RentalService.Domain.Repositories;
using RentalService.Presentation.Windows;

namespace RentalService.Presentation
{
    public class RentalServiceApplication
    {
        private readonly DomainManager _domainManager;
        private readonly OverviewWindow _overviewWindow;
        private readonly IEstablishmentRepository _establishmentRepository;


        public RentalServiceApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;

            _overviewWindow = new OverviewWindow();
            _overviewWindow.Show();
        }
    }

}
