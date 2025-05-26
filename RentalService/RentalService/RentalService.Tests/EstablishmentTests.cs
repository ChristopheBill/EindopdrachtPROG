using RentalService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Tests
{
    public class EstablishmentTests
    {
        [Fact]
        public void GetEstablishments_ReturnsEstablishmentDTOs()
        {
            // Arrange
            var domainManager = TestSetup.DomainManagerTestSetup.CreateDomainManagerWithTestMappers();
            
            // Act
            var establishments = domainManager.GetEstablishments();
            
            // Assert
            Assert.NotNull(establishments);
            Assert.IsType<List<EstablishmentDTO>>(establishments);
            Assert.NotEmpty(establishments);
        }
    }
}
