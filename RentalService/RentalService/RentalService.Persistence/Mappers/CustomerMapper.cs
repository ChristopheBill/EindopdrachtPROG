using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Persistence.Mappers
{
    public class CustomerMapper : ICustomerRepository
    {
        private readonly List<string> fouten = new();

        public List<Customer> GetCustomers()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Customers", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<Customer> customers = [];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    string email = (string)reader["Email"];
                    string street = (string)reader["Street"];
                    int postalCode = (int)reader["PostalCode"];
                    string city = (string)reader["City"];
                    string country = (string)reader["Country"];

                    Customer customer = new(id, firstName, lastName, email, street, postalCode, city, country);
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public void ReadCustomers(string pad)
        {
            string[] regels = File.ReadAllLines(pad);

            //if (regels.Length == 0 || regels[0] != "Voornaam;Achternaam;Email;Straat;Postcode;Woonplaats;Land")
            //{
            //    fouten.Add("Klanten.csv: ongeldige header.");
            //}

            using SqlConnection connection = new(DBInfo.ConnectionString);

            SqlCommand command = new SqlCommand("DELETE FROM Customers", connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Fout bij het verwijderen van de tabel: " + ex.Message);
            }
            finally { connection.Close(); }


            for (int i = 1; i < regels.Length; i++)
            {
                string[] delen = regels[i].Split(';');
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

                connection.Open();

                using SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using SqlCommand cmd = new("Insert into Customers (FirstName, LastName, Email, Street, PostalCode, City, Country) Values (@FirstName, @LastName, @Email, @Street, @PostalCode, @City, @Country)", connection, transaction);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Street", customer.StreetName);
                    cmd.Parameters.AddWithValue("@PostalCode", customer.ZipCode);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@Country", customer.Country);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Something went wrong {ex}");
                }
                finally 
                {
                    connection.Close(); 
                }
            }
        }
    }
}
