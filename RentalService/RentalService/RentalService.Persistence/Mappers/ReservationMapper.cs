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
        public List<Reservation> GetReservations()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Reservations", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<Reservation> reservations = [];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    DateTime startDate = (DateTime)reader["StartDate"];
                    DateTime endDate = (DateTime)reader["EndDate"];
                    int customerId = (int)reader["CustomerId"];
                    int carId = (int)reader["CarId"];
                    int establishmentId = (int)reader["EstablishmentId"];
                    Reservation reservation = new(id, startDate, endDate, customerId, carId, establishmentId);
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }
        public void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId) 
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            using SqlCommand MakeReservation = new("Insert into Reservations (StartDate, EndDate, CustomerId, CarId, EstablishmentId) VALUES (@StartDate, @EndDate, @CustomerId, @CarId, @EstablishmentId);", connection, transaction);
            MakeReservation.Parameters.Add(new SqlParameter("@StartDate", startDate));
            MakeReservation.Parameters.Add(new SqlParameter("@EndDate", endDate));
            MakeReservation.Parameters.Add(new SqlParameter("@CustomerId", customerId));
            MakeReservation.Parameters.Add(new SqlParameter("@CarId", carId));
            MakeReservation.Parameters.Add(new SqlParameter("@EstablishmentId", establishmentId));
            using SqlCommand SetCar = new("Insert into Cars (EstablishmentId) VALUES (@EstablishmentId) WHERE Id = @CarId;", connection, transaction);
            SetCar.Parameters.AddWithValue(("@CarId"), carId);
            SetCar.Parameters.Add(new SqlParameter("@EstablishmentId", establishmentId));

            MakeReservation.ExecuteNonQuery();
            SetCar.ExecuteScalar();
            transaction.Commit();
            connection.Close();
        }
    }
}
