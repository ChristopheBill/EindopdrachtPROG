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
            List<CustomerDTO> customers = new List<CustomerDTO>
            {
                new CustomerDTO(1, "Emma", "Johnson", "emma.johnson@example.com", "Main Street 1", "1000", "Brussel", "België"),
                new CustomerDTO(2, "Lucas", "Van den Berg", "lucas.vdb@example.com", "Stationsstraat 22", "2000", "Antwerpen", "België"),
                new CustomerDTO(3, "Sara", "Dubois", "sara.dubois@example.fr", "Rue Lafayette 12", "75009", "Parijs", "Frankrijk"),
                new CustomerDTO(4, "Noah", "Müller", "noah.mueller@example.de", "Bahnhofstraße 9", "60329", "Frankfurt", "Duitsland"),
                new CustomerDTO(5, "Léa", "Martin", "lea.martin@example.fr", "Avenue de France 8", "75013", "Parijs", "Frankrijk")
            };
            return customers;
        }

        public void ReadCustomers(string pad)
        {
            throw new NotImplementedException();
        }
    }
}
