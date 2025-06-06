﻿using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using System.Dynamic;
using System.Text;

namespace RentalService.Persistence.Mappers
{
    public class CarMapper : ICarRepository
    {
        private readonly List<string> _fouten = new();
        public List<CarDTO> GetCars()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Cars", connection);
            using SqlDataReader reader = command.ExecuteReader();
            List<CarDTO> cars = [];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cars.Add(MapReaderToCar(reader));
                }
            }
            return cars;
        }

        public List<CarDTO> GetCarsByEstablishment(int establishmentId)
        {
            List<CarDTO> cars = [];
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand getCarsByEstablishmentId = new("Select * from Cars where EstablishmentId = @EstablishmentId;", connection);
            getCarsByEstablishmentId.Parameters.AddWithValue("@EstablishmentId", establishmentId);
            SqlDataReader reader = getCarsByEstablishmentId.ExecuteReader();
            while (reader.Read())
            {
                cars.Add(MapReaderToCar(reader));
            }
            return cars;
        }

        public CarDTO GetCarById(int carId)
        {
            CarDTO car = new();
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand getCarById = new("Select * from Cars where Id = @CarId;", connection);
            getCarById.Parameters.AddWithValue("@CarId", carId);
            SqlDataReader reader = getCarById.ExecuteReader();
            while (reader.Read())
            {
                car = MapReaderToCar(reader);
            }
            return car;
        }

        public List<CarDTO> GetCarsBySeatsEstablishmentAvailability(int establishmentId, int seats, DateTime start, DateTime stop)
        {
            CarDTO car = new();
            List<CarDTO> cars = [];
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand getCarBySeatsEstablishment = new("Select * from Cars where EstablishmentId = @EstablishmentId AND Seats = @Seats;", connection);
            getCarBySeatsEstablishment.Parameters.AddWithValue("@EstablishmentId", establishmentId);
            getCarBySeatsEstablishment.Parameters.AddWithValue("@Seats", seats);
            SqlDataReader reader = getCarBySeatsEstablishment.ExecuteReader();
            while (reader.Read())
            {
                car = MapReaderToCar(reader);
                cars.Add(car);
            }
            CheckAvailability(cars, start, stop);
            return cars;
        }

        public void ReadCars(string pad)
        {
            string[] regels = File.ReadAllLines(pad);
            string? path = Path.GetDirectoryName(pad) ?? throw new Exception("Folder path is null.");
            string errorlogPath = Path.Combine(path, "ErrorLogCars.txt");

            if (File.Exists(errorlogPath))
            {
                File.Delete(errorlogPath);
            }
            List<string> fouten = new();
            HashSet<string> entries = new();
            using SqlConnection connection = new(DBInfo.ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Cars", connection);
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
                if (delen.Length < 4)
                {
                    fouten.Add($"Fout bij het inlezen van de auto op regel {i + 1}: Onvoldoende kolommen.");
                }
                Car car = new();
                EstablishmentDTO establishment = new EstablishmentMapper().GetEstablishmentById(InitialEstablishmentId(i));
                try
                {
                    car = new(
                        delen[0].Trim(),
                        delen[1].Trim(),
                        int.TryParse(delen[2], out int zp) ? zp : -1,
                        delen[3].Trim(), establishment
                        );
                }
                catch (Exception ex)
                {
                    fouten.Add($"Fout bij het aanmaken van de auto op regel {i + 1}: {ex.Message}");
                }
                string licensePlate = car.LicensePlate;
                if (entries.Contains(licensePlate))
                {
                    fouten.Add($"Fout bij het inlezen van de auto op regel {i + 1}: Nummerplaat '{licensePlate}' is al in gebruik.");
                    continue;
                }
                entries.Add(licensePlate);

                connection.Open();
                using SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using SqlCommand cmd = new("Insert into Cars (LicensePlate, Model, Seats, MotorType, EstablishmentId) Values (@LicensePlate, @Model, @Seats, @MotorType, @EstablishmentId)", connection, transaction);
                    cmd.Parameters.AddWithValue("@LicensePlate", car.LicensePlate);
                    cmd.Parameters.AddWithValue("@Model", car.Model);
                    cmd.Parameters.AddWithValue("@Seats", car.Seats);
                    cmd.Parameters.AddWithValue("@MotorType", car.MotorType);
                    cmd.Parameters.AddWithValue("@EstablishmentId", establishment.Id);
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
        public void GenerateMarkdown(int carId, int establishmentId)
        {
            CarDTO car = GetCarById(carId);
            EstablishmentDTO establishment = new EstablishmentMapper().GetEstablishmentById(establishmentId);
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string markdownPath = Path.Combine(basePath, "Markdown");
            if (!Directory.Exists(markdownPath))
            {
                Directory.CreateDirectory(markdownPath);
            }
            string safeDate = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filePath = Path.Combine(markdownPath, $"AutoOverzicht_{car.LicensePlate}_{car.Model}_{safeDate}.md");
            StringBuilder sb = new();
            sb.AppendLine("# Overzicht auto's");
            sb.AppendLine($"**Vestiging:** {establishment.Airport}");
            sb.AppendLine($"**Tijdstip:**{DateTime.Now}");
            sb.AppendLine();
            sb.AppendLine($"## Auto");
            sb.AppendLine($"**Model:** {car.Model}");
            sb.AppendLine($"**Nummerplaat:** {car.LicensePlate}");
            sb.AppendLine($"**Aantal zitplaatsen:** {car.Seats}");
            sb.AppendLine($"**Motor type:** {car.MotorType}");
            sb.AppendLine();
            sb.AppendLine("## Reserveringen");
            sb.AppendLine($"**Vorige reservering:**");
            if (CheckPreviousReservation(car))
            {
                sb.AppendLine($"**Periode:** {PreviousReservation(car).StartDate} - {PreviousReservation(car).EndDate}");
                sb.AppendLine($"**Klant:** {PreviousReservation(car).Customer.FirstName} {PreviousReservation(car).Customer.LastName}");
                sb.AppendLine($"**Adres klant:** {PreviousReservation(car).Customer.Street} {PreviousReservation(car).Customer.City} {PreviousReservation(car).Customer.Country}");
            }
            else
            {
                sb.AppendLine("Geen vorige reserveringen.");
            }
            sb.AppendLine($"**Volgende reservering:**");
            if (CheckNextReservation(car))
            {
                sb.AppendLine($"**Periode:** {NextReservation(car).StartDate} - {NextReservation(car).EndDate}");
                sb.AppendLine($"**Klant:** {NextReservation(car).Customer.FirstName} {NextReservation(car).Customer.LastName}");
                sb.AppendLine($"**Adres klant:** {NextReservation(car).Customer.Street} {NextReservation(car).Customer.City} {NextReservation(car).Customer.Country}");
            }
            else
            {
                sb.AppendLine("Geen volgende reserveringen.");
            }
            sb.AppendLine("---");
            File.WriteAllText(filePath, sb.ToString());
        }

        private CarDTO MapReaderToCar(SqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string licensePlate = (string)reader["LicensePlate"];
            string model = (string)reader["Model"];
            int seats = (int)reader["Seats"];
            string motorType = (string)reader["MotorType"];
            int establishmentId = (int)reader["EstablishmentId"];
            EstablishmentDTO establishment = new EstablishmentMapper().GetEstablishmentById(establishmentId);

            return new CarDTO(id, licensePlate, model, seats, motorType, establishment);
        }

        private int InitialEstablishmentId(int carIndex)
        {
            EstablishmentMapper establishmentMapper = new();
            List<EstablishmentDTO> establishments = establishmentMapper.GetEstablishments();
            int aantalVestigingen = establishments.Count();
            EstablishmentDTO establishment = establishments[carIndex % aantalVestigingen];
            int establishmentId = establishment.Id;
            return establishmentId;
        }

        private List<CarDTO> CheckAvailability(List<CarDTO> cars, DateTime start, DateTime stop)
        {
            ReservationMapper reservationMapper = new();
            List<ReservationDTO> reservations = reservationMapper.GetReservations();
            var carsToRemove = new List<CarDTO>();
            foreach (CarDTO c in cars)
            {
                foreach (ReservationDTO r in reservations)
                {
                    if (c.Id == r.Car.Id && r.StartDate < stop && r.EndDate > start)
                    {
                        carsToRemove.Add(c);
                        break;
                    }
                }
            }
            foreach (CarDTO c in carsToRemove)
            {
                cars.Remove(c);
            }
            return cars;
        }

        private bool CheckPreviousReservation (CarDTO car)
        {
            ReservationMapper reservationMapper = new();
            List<ReservationDTO> reservations = reservationMapper.GetReservations();
            foreach (ReservationDTO r in reservations)
            {
                if (r.Car.Id == car.Id && r.EndDate <= DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
        private ReservationDTO PreviousReservation(CarDTO car)
        {
            ReservationMapper reservationMapper = new();
            List<ReservationDTO> reservations = reservationMapper.GetReservations();
            ReservationDTO previousReservation = new();
            foreach (ReservationDTO r in reservations)
            {
                if (r.Car.Id == car.Id && r.EndDate <= DateTime.Now)
                {
                    previousReservation = r;
                }
            }
            return previousReservation;
        }

        private bool CheckNextReservation(CarDTO car)
        {
            ReservationMapper reservationMapper = new();
            List<ReservationDTO> reservations = reservationMapper.GetReservations();
            foreach (ReservationDTO r in reservations)
            {
                if (r.Car.Id == car.Id && r.StartDate >= DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        private ReservationDTO NextReservation(CarDTO car)
        {
            ReservationMapper reservationMapper = new();
            List<ReservationDTO> reservations = reservationMapper.GetReservations();
            ReservationDTO nextReservation = new();
            foreach (ReservationDTO r in reservations)
            {
                if (r.Car.Id == car.Id && r.StartDate >= DateTime.Now)
                {
                    nextReservation = r;
                }
            }
            return nextReservation;
        }
    }
}
