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
        public List<Car> GetCars()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM People", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<Car> people = [];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];
                    DateOnly birthDate = DateOnly.FromDateTime((DateTime)reader["BirthDate"]);
                    people.Add(new Person(id, name, birthDate));
                }
            }

            return people;
        }
    }
}
