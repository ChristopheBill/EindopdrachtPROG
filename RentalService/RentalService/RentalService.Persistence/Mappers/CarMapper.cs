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
                    //int id = (int)reader["Id"];
                    //string name = (string)reader["Name"];
                    //DateOnly birthDate = DateOnly.FromDateTime((DateTime)reader["BirthDate"]);
                    //people.Add(new Person(id, name, birthDate));
                }
            }

            return cars;
        }

        public void LeesAutos(string pad)
        {
            var regels = File.ReadAllLines(pad);

            if (regels.Length == 0 || regels[0] != "LicensePlate;Model;Seats;Motortype")
            {
                fouten.Add("Autos.csv: ongeldige header.");
            }

            for (int i = 1; i < regels.Length; i++)
            {
                var delen = regels[i].Split(';');
                if (delen.Length < 4)
                {
                    fouten.Add($"Autos.csv - Regel {i + 1}: Onvoldoende kolommen.");
                    continue;
                }

                Car car = new(
                    delen[0],
                    delen[1],
                    int.TryParse(delen[2], out var zp) ? zp : -1,
                    delen[3]);

                try
                {
                    using SqlConnection connection = new(DBInfo.ConnectionString);

                    using SqlCommand command = new("Insert into Cars (LicensePlate, Model, Seats, MotorType) Values (@LicensePlate, @Model, @Seats, @MotorType)", connection);
                    command.Parameters.AddWithValue("@LicensePlate", car.LincensePlate);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@Seats", car.Seats);
                    command.Parameters.AddWithValue("@MotorType", car.MotorType);

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
