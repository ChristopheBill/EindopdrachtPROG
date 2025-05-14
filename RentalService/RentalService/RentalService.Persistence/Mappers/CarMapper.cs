using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Persistence.Mappers
{
    public class CarMapper : ICarRepository
    {
        private readonly List<string> fouten = new();
        //private SqlConnection _connection = new SqlConnection(DBInfo.ConnectionString);

        public List<Car> GetCars()
        {
            //using SqlConnection connection = _connection;
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Cars", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<Car> cars = [];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string licensePlate = (string)reader["LicensePlate"];
                    string model = (string)reader["Model"];
                    int seats = (int)reader["Seats"];
                    string motorType = (string)reader["MotorType"];
                    Car car = new(id, licensePlate, model, seats, motorType);
                    cars.Add(car);
                }
            }
            return cars;
        }

        public List<Car> GetCarsByEstablishment(int establishmentId)
        {
            List<Car> cars = [];
            using SqlConnection connection = new(DBInfo.ConnectionString);
            using SqlCommand getCarsByEstablishmentId = new("Select * from Cars where Id = @PlayerId", connection);


            return cars;
        }


        public void ReadCars(string pad)
        {
            string[] regels = File.ReadAllLines(pad);

            using SqlConnection connection = new(DBInfo.ConnectionString);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Cars", connection);
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
                if (delen.Length < 4)
                {
                    fouten.Add($"Autos.csv - Regel {i + 1}: Onvoldoende kolommen.");
                    continue;
                }

                Car car = new(
                    delen[0],
                    delen[1],
                    int.TryParse(delen[2], out int zp) ? zp : -1,
                    delen[3]);

                connection.Open();
                using SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using SqlCommand cmd = new("Insert into Cars (LicensePlate, Model, Seats, MotorType) Values (@LicensePlate, @Model, @Seats, @MotorType)", connection, transaction);
                    cmd.Parameters.AddWithValue("@LicensePlate", car.LicensePlate);
                    cmd.Parameters.AddWithValue("@Model", car.Model);
                    cmd.Parameters.AddWithValue("@Seats", car.Seats);
                    cmd.Parameters.AddWithValue("@MotorType", car.MotorType);

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
