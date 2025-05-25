using RentalService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public List<CustomerDTO> GetCustomers();
        public void ReadCustomers(string pad);
        public CustomerDTO GetCustomerById(int customerId);
    }
}
