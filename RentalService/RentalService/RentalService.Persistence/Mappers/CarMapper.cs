using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;

namespace RentalService.Persistence.Mappers
{
    public class CarMapper : ICarRepository
    {
        private readonly List<string> _fouten = new();
        private IEstablishmentRepository _establishmentRepository;
        public List<Car> GetCars()
        {
            using SqlConnection connection = new(DBInfo.ConnectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM Cars", connection);
            using SqlDataReader reader = command.ExecuteReader();
            List<Car> cars = [];

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cars.Add(MapReaderToCar(reader));
                }
            }
            return cars;
        }

        public List<Car> GetCarsByEstablishment(int establishmentId)
        {
            List<Car> cars = [];
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

        public Car GetCarById(int carId)
        {
            Car car = new();
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
        //public List<Car> GetCarsById(int carId)
        //{
        //    Car car = new();
        //    List<Car> cars = [];
        //    using SqlConnection connection = new(DBInfo.ConnectionString);
        //    connection.Open();
        //    using SqlCommand getCarById = new("Select * from Cars where Id = @CarId;", connection);
        //    getCarById.Parameters.AddWithValue("@CarId", carId);
        //    SqlDataReader reader = getCarById.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        car = MapReaderToCar(reader);
        //        cars.Add(car);
        //    }
        //    return cars;
        //}


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
                    fouten.Add($"Fout bij het inlezen van de auto op regel {i+1}: Onvoldoende kolommen.");
                }
                Car car = new();
                Establishment establishment = new EstablishmentMapper().GetEstablishmentById(InitialEstablishmentId(i));
                try
                {
                    car = new(
                        delen[0],
                        delen[1],
                        int.TryParse(delen[2], out int zp) ? zp : -1,
                        delen[3],establishment
                        );
                }
                catch (Exception ex)
                {
                    fouten.Add($"Fout bij het aanmaken van de auto op regel {i + 1}: {ex.Message}");
                }
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

        private Car MapReaderToCar(SqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string licensePlate = (string)reader["LicensePlate"];
            string model = (string)reader["Model"];
            int seats = (int)reader["Seats"];
            string motorType = (string)reader["MotorType"];
            int establishmentId = (int)reader["EstablishmentId"];
            Establishment establishment = new EstablishmentMapper().GetEstablishmentById(establishmentId);

            return new Car(id, licensePlate, model, seats, motorType, establishment);
        }

        private int InitialEstablishmentId(int carIndex)
        {
            _establishmentRepository = new EstablishmentMapper();
            List<Establishment> establishments = _establishmentRepository.GetEstablishments();
            int aantalVestigingen = establishments.Count();
            Establishment establishment = establishments[carIndex % aantalVestigingen];
            int establishmentId = establishment.Id;
            return establishmentId;
        }
    }
}
