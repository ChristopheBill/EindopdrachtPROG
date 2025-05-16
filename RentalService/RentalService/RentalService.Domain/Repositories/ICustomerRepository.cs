using RentalService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public List<Customer> GetCustomers();
        public void ReadCustomers(string pad);
        public Customer GetCustomerById(int customerId);
    }
}
