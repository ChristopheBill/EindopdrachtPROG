using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Persistence.Mappers
{
    public class CustomerMapper : ICustomerRepository
    {
        private readonly List<string> fouten = new();

        public void ReadCustomers(string pad)
        {
            var regels = File.ReadAllLines(pad);

            if (regels.Length == 0 || regels[0] != "Voornaam;Achternaam;Email;Straat;Postcode;Woonplaats;Land")
            {
                fouten.Add("Klanten.csv: ongeldige header.");
            }

            for (int i = 1; i < regels.Length; i++)
            {
                var delen = regels[i].Split(';');
                if (delen.Length < 7)
                {
                    fouten.Add($"Klanten.csv - Regel {i + 1}: Onvoldoende kolommen.");
                    continue;
                }

                Customer customer = new(
                    delen[0],
                    delen[1],
                    delen[2],
                    delen[3],
                    int.Parse(delen[4]),
                    delen[5],
                    delen[6]);

                try
                {
                    using SqlConnection connection = new(DBInfo.ConnectionString);

                    using SqlCommand command = new("Insert into Customers (FirstName, LastName, Email, Street, PostalCode, City, Country) Values (@FirstName, @LastName, @Email, @Street, @PostalCode, @City, @Country)", connection);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Street", customer.StreetName);
                    command.Parameters.AddWithValue("@PostalCode", customer.ZipCode);
                    command.Parameters.AddWithValue("@City", customer.City);
                    command.Parameters.AddWithValue("@Country", customer.Country);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Something went wrong {ex}");
                }
            }
        }
    }
}
