using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Persistence.Mappers
{
    public class CarMapper : ICarRepository
    {
        private readonly List<string> fouten = new();

        public List<Car> GetCars()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM People", connection);
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

        public void ReadCars(string pad)
        {
            string[] regels = File.ReadAllLines(pad);

            //if (regels.Length == 0 || regels[0] != "LicensePlate;Model;Seats;Motortype")
            //{
            //    fouten.Add("Autos.csv: ongeldige header.");
            //}

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

                using SqlConnection connection = new(DBInfo.ConnectionString);
                connection.Open();
                using SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using SqlCommand command = new("Insert into Cars (LicensePlate, Model, Seats, MotorType) Values (@LicensePlate, @Model, @Seats, @MotorType)", connection, transaction);
                    command.Parameters.AddWithValue("@LicensePlate", car.LicensePlate);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@Seats", car.Seats);
                    command.Parameters.AddWithValue("@MotorType", car.MotorType);

                    //command.ExecuteNonQuery();

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
