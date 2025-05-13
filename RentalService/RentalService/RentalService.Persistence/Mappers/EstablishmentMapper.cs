using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Persistence.Mappers
{
    public class EstablishmentMapper : IEstablishmentRepository
    {
        private readonly List<string> fouten = new();

        public List<Establishment> GetEstablishments()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Establishments", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<Establishment> establishments = [];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string airport = (string)reader["Airport"];
                    string street = (string)reader["Street"];
                    string postalCode = (string)reader["PostalCode"];
                    string city = (string)reader["City"];
                    string country = (string)reader["Country"];

                    Establishment establishment = new(id, airport, street, postalCode, city, country);
                    establishments.Add(establishment);
                }
            }
            return establishments;
        }
        public void ReadEstablishments(string pad)
        {
            string[] regels = File.ReadAllLines(pad);
            Establishment location;

            //if (regels.Length == 0 || regels[0] != "Airport;Street;PostalCose;City;Country")
            //{
            //    fouten.Add("Vestigingen.csv: ongeldige header.");
            //}
            using SqlConnection connection = new(DBInfo.ConnectionString);

            SqlCommand command = new SqlCommand("DELETE FROM Establishments", connection);

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
                if (delen.Length < 5)
                {
                    fouten.Add($"Vestigingen.csv - Regel {i + 1}: Onvoldoende kolommen.");
                    continue;
                }

                location = new(
                    delen[0],
                    delen[1],
                    delen[2],
                    delen[3],
                    delen[4]);

                connection.Open();
                using SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using SqlCommand cmd = new("Insert into Establishments (Airport, Street, PostalCode, City, Country) Values (@Airport, @Street, @PostalCode, @City, @Country)", connection, transaction);
                    cmd.Parameters.AddWithValue("@Airport", location.Airport);
                    cmd.Parameters.AddWithValue("@Street", location.Street);
                    cmd.Parameters.AddWithValue("@PostalCode", location.PostalCode);
                    cmd.Parameters.AddWithValue("@City", location.City);
                    cmd.Parameters.AddWithValue("@Country", location.Country);

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
