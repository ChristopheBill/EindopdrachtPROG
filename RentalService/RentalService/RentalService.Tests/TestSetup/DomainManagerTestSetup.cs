using RentalService.Domain;
using RentalService.Domain.Repositories;
using RentalService.Tests.TestMappers;

namespace RentalService.Tests.TestSetup
{
    public class DomainManagerTestSetup
    {
            public static DomainManager CreateDomainManagerWithTestMappers()
            {
            ICarRepository carRepository = new TestCarMapper();
            ICustomerRepository customerRepository = new TestCustomerMapper();
            IEstablishmentRepository establishmentRepository = new TestEstablishmentMapper();
            IReservationRepository reservationRepository = new TestReservationMapper();

            return new DomainManager(
                    carRepository,
                    customerRepository,
                    establishmentRepository,
                    reservationRepository
                );
        }
    }
}
