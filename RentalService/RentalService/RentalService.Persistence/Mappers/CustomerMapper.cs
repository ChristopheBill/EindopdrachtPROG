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
                    string postalCode = (string)reader["PostalCode"];
                    string city = (string)reader["City"];
                    string country = (string)reader["Country"];

                    Customer customer = new(id, firstName, lastName, email, street, postalCode, city, country);
                    customers.Add(customer);
                }
            }
            return customers;
        }
        public Customer GetCustomerById(int customerId)
        {
            Customer customer = new();
            using SqlConnection connection = new(DBInfo.ConnectionString);
            using SqlCommand getCustomerById = new("Select * from Customers where Id = @Id", connection);
            getCustomerById.Parameters.AddWithValue("@Id", customerId);
            connection.Open();
            using SqlDataReader reader = getCustomerById.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    string email = (string)reader["Email"];
                    string street = (string)reader["Street"];
                    string postalCode = (string)reader["PostalCode"];
                    string city = (string)reader["City"];
                    string country = (string)reader["Country"];
                    customer = new(id, firstName, lastName, email, street, postalCode, city, country);
                    return customer;
                }
            }
            return customer;
        }

        public void ReadCustomers(string pad)
        {
            string[] regels = File.ReadAllLines(pad);
            string? path = Path.GetDirectoryName(pad) ?? throw new Exception("Folder path is null.");
            string errorlogPath = Path.Combine(path, "ErrorLogCustomers.txt");
            List<string> fouten = new();
            HashSet<string> entries = new();
            using SqlConnection connection = new(DBInfo.ConnectionString);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Customers", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                fouten.Add($"Fout bij het verwijderen van de tabel: {ex.Message}");
                //Console.WriteLine("Fout bij het verwijderen van de tabel: " + ex.Message);
            }
            finally { connection.Close(); }


            for (int i = 1; i < regels.Length; i++)
            {
                string[] delen = regels[i].Split(';');
                if (delen.Length < 7)
                {
                    fouten.Add($"Fout bij het inlezen van de klant op regel {i + 1}: Onvoldoende kolommen.");
                    //continue;
                }
                Customer customer = new();
                try
                {
                    customer = new(
                    delen[0],
                    delen[1],
                    delen[2],
                    delen[3],
                    delen[4],
                    delen[5],
                    delen[6]);
                }
                catch (SqlException ex)
                {
                    fouten.Add("Fout bij het inlezen van de klant: " + ex.Message);
                    //continue;
                }
                string customerEntry = $"{customer.FirstName};{customer.LastName};{customer.Email};{customer.Street};{customer.PostalCode};{customer.City};{customer.Country}".ToLower();
                if (entries.Contains(customerEntry))
                {
                    fouten.Add($"Fout bij het inlezen van de klant op regel {i + 1}: Dubbele klant.");
                }
                    //continue;
                

                connection.Open();

                using SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using SqlCommand cmd = new("Insert into Customers (FirstName, LastName, Email, Street, PostalCode, City, Country) Values (@FirstName, @LastName, @Email, @Street, @PostalCode, @City, @Country)", connection, transaction);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Street", customer.Street);
                    cmd.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@Country", customer.Country);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    fouten.Add($"Fout bij het toevoegen van de klant: {ex.Message}");
                }
                finally 
                {
                    connection.Close(); 
                }
                if (fouten.Count > 0)
                {
                    using StreamWriter writer = new(errorlogPath, true);
                    foreach (string fout in fouten)
                    {
                        writer.WriteLine(fout);
                    }
                }
            }
        }
    }
}
