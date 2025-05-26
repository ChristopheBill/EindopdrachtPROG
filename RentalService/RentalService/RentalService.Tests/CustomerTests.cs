using RentalService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void GetCustomers_ReturnsCustomerDTOs()
        {
            // Arrange
            var domainManager = TestSetup.DomainManagerTestSetup.CreateDomainManagerWithTestMappers();
            // Act
            var customers = domainManager.GetCustomers();
            // Assert
            Assert.NotNull(customers);
            Assert.IsType<List<CustomerDTO>>(customers);
            Assert.NotEmpty(customers);
        }
    }
}
