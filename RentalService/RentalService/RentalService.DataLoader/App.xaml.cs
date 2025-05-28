using RentalService.Domain.Repositories;
using RentalService.Domain;
using RentalService.Persistence.Mappers;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RentalService.DataLoader
{
    // Interaction logic for App.xaml
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Persistence
            ICarRepository carRepository = new CarMapper();
            ICustomerRepository customerRepository = new CustomerMapper();
            IEstablishmentRepository locationRepository = new EstablishmentMapper();
            IReservationRepository reservationRepository = new ReservationMapper();

            //Domain
            DomainManager domain = new(carRepository, customerRepository, locationRepository, reservationRepository);

            //Presentation
            DataLoaderApplication application = new(domain);
        }
    }

}
