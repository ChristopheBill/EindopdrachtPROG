using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.DataClassification;
using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Persistence.Mappers
{
    public class EstablishmentMapper : IEstablishmentRepository
    {
        public List<EstablishmentDTO> GetEstablishments()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Establishments", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<EstablishmentDTO> establishments = [];

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

                    EstablishmentDTO establishment = new(id, airport, street, postalCode, city, country);
                    establishments.Add(establishment);
                }
            }
            return establishments;
        }
        public EstablishmentDTO GetEstablishmentById(int establishmentId)
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Establishments WHERE Id = @Id", connection);
            command.Parameters.Add(new SqlParameter("@Id", establishmentId));
            using SqlDataReader reader = command.ExecuteReader();
            EstablishmentDTO establishment = new();
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
                    establishment = new(id, airport, street, postalCode, city, country);
                }
            }
            return establishment;
        }
        public void ReadEstablishments(string pad)
        {
            string[] regels = File.ReadAllLines(pad);
            string? path = Path.GetDirectoryName(pad) ?? throw new Exception("Folder path is null.");
            string errorlogPath = Path.Combine(path, "ErrorLogEstablishments.txt");
            if (File.Exists(errorlogPath))
            {
                File.Delete(errorlogPath);
            }
            List<string> fouten = new();
            using SqlConnection connection = new(DBInfo.ConnectionString);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Establishments", connection);
                SqlCommand deleteReservations = new SqlCommand("DELETE FROM Reservations", connection);
                connection.Open();
                command.ExecuteNonQuery();
                deleteReservations.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                fouten.Add("Fout bij het verwijderen van de tabel: " + ex.Message);
            }
            finally { connection.Close(); }

            for (int i = 1; i < regels.Length; i++)
            {
                string[] delen = regels[i].Split(';');
                if (delen.Length < 5)
                {
                    fouten.Add($"Fout bij het inlezen van de vestiging op regel {i + 1}: Onvoldoende kolommen.");
                }
                Establishment location = new();
                try
                {
                    location = new(
                        delen[0].Trim(),
                        delen[1].Trim(),
                        delen[2].Trim(),
                        delen[3].Trim(),
                        delen[4].Trim());
                }
                catch (Exception ex)
                {
                    fouten.Add($"Fout bij het inlezen van de vestiging op regel {i + 1}: {ex.Message}");
                }
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
