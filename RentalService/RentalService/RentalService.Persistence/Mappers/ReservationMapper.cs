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
    public class ReservationMapper : IReservationRepository
    {
        //public List<Establishment> GetReservations(Customer customer, Car car, Establishment establishment)
        //{
        //    using SqlConnection connection = new(DBInfo.ConnectionString);
        //    connection.Open();
        //    using SqlCommand command = new("SELECT * FROM Reservations", connection);
        //    command.Parameters.AddWithValue("@CarId", car.Id);
        //    command.Parameters.AddWithValue("@CustomerId", customer.Id);
        //    command.Parameters.AddWithValue("@BranchId", establishment.Id);

        //    using SqlDataReader reader = command.ExecuteReader();

        //    List<Reservation> reservations = [];

        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            int id = (int)reader["Id"];
        //            DateTime startDate = (DateTime)reader["StartDate"];
        //            DateTime endDate = (DateTime)reader["EndDate"];

        //            Reservation reservation = new(id, startDate, endDate, customer, car, establishment);
        //            reservations.Add(reservation);
        //        }
        //    }
        //    return reservations;
        //}
    }
}
