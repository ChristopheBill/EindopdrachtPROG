using RentalService.Domain.DTOs;

namespace RentalService.Tests
{
    public class DomainManagerTests
    {
        private readonly DomainManager _domainManager;
        private readonly Mock<ICarRepository> _carRepoMock;
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly Mock<IEstablishmentRepository> _establishmentRepoMock;
        private readonly Mock<IReservationRepository> _reservationRepoMock;

        public DomainManagerTests()
        {
            _carRepoMock = new Mock<ICarRepository>();
            _customerRepoMock = new Mock<ICustomerRepository>();
            _establishmentRepoMock = new Mock<IEstablishmentRepository>();
            _reservationRepoMock = new Mock<IReservationRepository>();

            _domainManager = new DomainManager(
                _carRepoMock.Object,
                _customerRepoMock.Object,
                _establishmentRepoMock.Object,
                _reservationRepoMock.Object
            );
        }

        [Fact]
        public void GetCars_ReturnsCarDTOs()
        {
            var cars = new List<Car>
        {
            new Car { Id = 1, Model = "Test Car 1" },
            new Car { Id = 2, Model = "Test Car 2" }
        };
            _carRepoMock.Setup(repo => repo.GetCars()).Returns(cars);

            var result = _domainManager.GetCars();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Model == "Test Car 1");
        }

        [Fact]
        public void GetCustomers_ReturnsCustomerDTOs()
        {
            var customers = new List<Customer>
        {
            new Customer("John", "Doe", "john.doe@example.com", "Street", "1234", "City", "Country"),
            new Customer("Jane", "Smith", "jane.smith@example.com", "Street", "5678", "City", "Country")
        };

            _customerRepoMock.Setup(repo => repo.GetCustomers()).Returns(customers);

            var result = _domainManager.GetCustomers();

            Assert.Equal(2, result.Count);
            Assert.Equal("John", result[0].FirstName);
        }

        [Fact]
        public void MakeReservation_CallsRepositoryWithCorrectParams()
        {
            var start = new DateTime(2025, 1, 1);
            var end = new DateTime(2025, 1, 10);

            _domainManager.MakeReservation(start, end, 1, 2, 3);

            _reservationRepoMock.Verify(r => r.MakeReservation(start, end, 1, 2, 3), Times.Once);
        }
    }
}
