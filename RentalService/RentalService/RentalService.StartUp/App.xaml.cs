using RentalService.Domain;
using RentalService.Domain.Repositories;
using RentalService.Persistence.Mappers;
using RentalService.Presentation;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RentalService.StartUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Persistence
            ICarRepository carRepository = new CarMapper();
            ICustomerRepository customerRepository = new CustomerMapper();
            ILocationRepository locationRepository = new EstablishmentMapper();

            //Domain
            DomainManager domain = new(carRepository, customerRepository, locationRepository);

            //Presentation
            RentalServiceApplication application = new(domain);
        }
    }

}
