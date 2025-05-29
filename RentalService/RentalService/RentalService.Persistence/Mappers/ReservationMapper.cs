using Microsoft.Data.SqlClient;
using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.Persistence.Mappers
{
    public class ReservationMapper : IReservationRepository
    {
        public List<ReservationDTO> GetReservations()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Reservations", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<ReservationDTO> reservations = [];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    ReservationDTO reservation = MapReaderToReservation(reader);
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }
        public List<ReservationDTO> GetReservationsByCustomerIdEstablishmentIdDate(int customerId, int establishmentId, DateTime date)
        {
            List<ReservationDTO> reservations = new();
            using SqlConnection connection = new( DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Reservations WHERE CustomerId = @CustomerId AND EstablishmentId = @EstablishmentId;", connection);
            command.Parameters.Add(new SqlParameter("@CustomerId", customerId));
            command.Parameters.Add(new SqlParameter("@EstablishmentId", establishmentId));
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ReservationDTO reservation = MapReaderToReservation(reader);
                    reservations.Add(reservation);
                }
            }
            List<ReservationDTO> filteredReservations = reservations.Where(r => (r.StartDate <= date && r.EndDate >= date)).ToList();
            return filteredReservations;


        }
        public void MakeReservation(DateTime startDate, DateTime endDate, int customerId, int carId, int establishmentId) 
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            try 
            {
            using SqlCommand MakeReservation = new("Insert into Reservations (StartDate, EndDate, CustomerId, CarId, EstablishmentId) VALUES (@StartDate, @EndDate, @CustomerId, @CarId, @EstablishmentId);", connection, transaction);
            MakeReservation.Parameters.Add(new SqlParameter("@StartDate", startDate));
            MakeReservation.Parameters.Add(new SqlParameter("@EndDate", endDate));
            MakeReservation.Parameters.Add(new SqlParameter("@CustomerId", customerId));
            MakeReservation.Parameters.Add(new SqlParameter("@CarId", carId));
            MakeReservation.Parameters.Add(new SqlParameter("@EstablishmentId", establishmentId));
            using SqlCommand SetCar = new("UPDATE Cars SET EstablishmentId = @EstablishmentId WHERE Id = @CarId;", connection, transaction);
            SetCar.Parameters.AddWithValue(("@CarId"), carId);
            SetCar.Parameters.Add(new SqlParameter("@EstablishmentId", establishmentId));
            MakeReservation.ExecuteNonQuery();
            SetCar.ExecuteScalar();

            transaction.Commit();

            connection.Close();
                }
            catch (Exception ex) 
            {
                transaction.Rollback();
                throw new Exception("Fout bij het maken van de reservering: " + ex.Message);
            }
        }
        public void RemoveReservation (int reservationId)
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand RemoveReservation = new("Delete from Reservations where Id = @Id;", connection);
            RemoveReservation.Parameters.Add(new SqlParameter("@Id", reservationId));
            RemoveReservation.ExecuteNonQuery();
        }
        private ReservationDTO MapReaderToReservation(SqlDataReader reader) 
        {
            int id = (int)reader["Id"];
            DateTime startDate = (DateTime)reader["StartDate"];
            DateTime endDate = (DateTime)reader["EndDate"];
            int customerId = (int)reader["CustomerId"];
            int carId = (int)reader["CarId"];
            int establishmentId = (int)reader["EstablishmentId"];
            CustomerDTO customer = new CustomerMapper().GetCustomerById(customerId);
            CarDTO car = new CarMapper().GetCarById(carId);
            EstablishmentDTO establishment = new EstablishmentMapper().GetEstablishmentById(establishmentId);
            return new ReservationDTO(id, startDate, endDate, customer, car, establishment);
        }
    }
}
