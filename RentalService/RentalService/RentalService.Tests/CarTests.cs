using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalService.Domain.DTOs;
using RentalService.Domain;

namespace RentalService.Tests
{
    public class CarTests
    {
        [Fact]
        public void GetCars_ReturnsCarDTOs()
        {
            // Arrange
            var domainManager = TestSetup.DomainManagerTestSetup.CreateDomainManagerWithTestMappers();

            // Act
            var cars = domainManager.GetCars();

            // Assert
            Assert.NotNull(cars);
            Assert.IsType<List<CarDTO>>(cars);
            Assert.NotEmpty(cars);
        }
        [Fact]
        public void GetCarsByEstablishment_ReturnsCorrectCars()
        {
            // Arrange
            var domainManager = TestSetup.DomainManagerTestSetup.CreateDomainManagerWithTestMappers();
            int establishmentId = 1; // Assuming establishment with ID 1 exists in the test data
            // Act
            var cars = domainManager.GetCarsByEstablishment(establishmentId);
            // Assert
            Assert.NotNull(cars);
            Assert.All(cars, car => Assert.Equal(establishmentId, car.Establishment.Id));
        }
    }
}
