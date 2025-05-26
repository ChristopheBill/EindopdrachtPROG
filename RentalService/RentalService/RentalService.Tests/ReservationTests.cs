using RentalService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Tests
{
    public class ReservationTests
    {
        [Fact]
        public void GetReservations_ReturnsReservationDTOs()
        {
            // Arrange
            var domainManager = TestSetup.DomainManagerTestSetup.CreateDomainManagerWithTestMappers();
            
            // Act
            var reservations = domainManager.GetReservations();
            
            // Assert
            Assert.NotNull(reservations);
            Assert.IsType<List<ReservationDTO>>(reservations);
            Assert.NotEmpty(reservations);
        }
    }
}
