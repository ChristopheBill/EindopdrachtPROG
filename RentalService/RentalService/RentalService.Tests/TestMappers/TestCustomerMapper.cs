using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Tests.TestMappers
{
    class TestCustomerMapper : ICustomerRepository
    {
        public CustomerDTO GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }

        public List<CustomerDTO> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public void ReadCustomers(string pad)
        {
            throw new NotImplementedException();
        }
    }
}
