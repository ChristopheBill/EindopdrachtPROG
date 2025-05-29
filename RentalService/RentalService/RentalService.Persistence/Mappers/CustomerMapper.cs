using Microsoft.Data.SqlClient;
using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Persistence.Mappers
{
    public class CustomerMapper : ICustomerRepository
    {
        private readonly List<string> fouten = new();

        public List<CustomerDTO> GetCustomers()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Customers", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<CustomerDTO> customers = [];

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

                    CustomerDTO customer = new(id, firstName, lastName, email, street, postalCode, city, country);
                    customers.Add(customer);
                }
            }
            return customers;
        }
        public CustomerDTO GetCustomerById(int customerId)
        {
            CustomerDTO customer = new();
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
            if (File.Exists(errorlogPath))
            {
                File.Delete(errorlogPath);
            }
            HashSet<string> fouten = new();
            HashSet<string> entries = new();
            using SqlConnection connection = new(DBInfo.ConnectionString);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Customers", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                fouten.Add($"Fout bij het verwijderen van de tabel: {ex.Message}");
            }
            finally { connection.Close(); }


            for (int i = 1; i < regels.Length; i++)
            {
                string[] delen = regels[i].Split(';');
                if (delen.Length < 7)
                {
                    fouten.Add($"Fout bij het inlezen van de klant op regel {i + 1}: Onvoldoende kolommen.");
                }
                Customer customer = new();
                try
                {
                    customer = new(
                    delen[0].Trim(),
                    delen[1].Trim(),
                    delen[2].Trim(),
                    delen[3].Trim(),
                    delen[4].Trim(),
                    delen[5].Trim(),
                    delen[6].Trim());
                }
                catch (Exception ex)
                {
                    fouten.Add($"Fout bij het inlezen van de klant op regel {i + 1}: " + ex.Message);
                }
                string customerEmail = customer.Email;

                if (entries.Contains(customerEmail))
                {
                    fouten.Add($"Fout bij het inlezen van de klant op regel {i + 1}: Dubbel emailadres.");
                }
                entries.Add(customerEmail);
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
                    catch (SqlException)
                    {
                        transaction.Rollback();
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
