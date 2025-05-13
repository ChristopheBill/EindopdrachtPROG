using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalService.DataLoader
{
    public class DataLoaderApplication
    {
        private readonly HashSet<string> uniekeEmails = new();
        private readonly HashSet<string> uniekeNummerplaten = new();
        private readonly List<string> fouten = new();

        public List<string> InitialiseAllFiles(string padVestigingen, string padAutos, string padKlanten)
        {
            fouten.Clear();
            uniekeEmails.Clear();
            uniekeNummerplaten.Clear();
            LeesVestigingen(padVestigingen);
            LeesAutos(padAutos);
            LeesKlanten(padKlanten);

            return fouten;
        }



        public void LeesVestigingen(string pad)
        {
            var regels = File.ReadAllLines(pad);
            Establishment location;

            if (regels.Length == 0 || regels[0] != "Airport;Street;PostalCose;City;Country")
            {
                fouten.Add("Vestigingen.csv: ongeldige header.");
            }

            for (int i = 1; i < regels.Length; i++)
            {
                var delen = regels[i].Split(';');
                if (delen.Length < 5)
                {
                    fouten.Add($"Vestigingen.csv - Regel {i + 1}: Onvoldoende kolommen.");
                    continue;
                }

                location = new(
                    delen[0],
                    delen[1],
                    int.Parse(delen[2]),
                    int.Parse(delen[3]),
                    delen[4]);
                try
                {
                    using SqlConnection connection = new(DBInfo.ConnectionString);

                    using SqlCommand command = new("Insert into Establishments (Airport, Street, PostalCode, City, Country) Values (@Airport, @Street, @PostalCode, @City, @Country)", connection);
                    command.Parameters.AddWithValue("@Airport", location.Airport);
                    command.Parameters.AddWithValue("@Street", location.StreetName);
                    command.Parameters.AddWithValue("@PostalCode", location.PostalCode);
                    command.Parameters.AddWithValue("@City", location.City);
                    command.Parameters.AddWithValue("@Country", location.Country);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Something went wrong {ex}");
                }

            }

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

        public void LeesKlanten(string pad)
        {
            var regels = File.ReadAllLines(pad);

            if (regels.Length == 0 || regels[0] != "Voornaam;Achternaam;Email;Straat;Postcode;Woonplaats;Land")
            {
                fouten.Add("Klanten.csv: ongeldige header.");
            }

            for (int i = 1; i < regels.Length; i++)
            {
                var delen = regels[i].Split(';');
                if (delen.Length < 7)
                {
                    fouten.Add($"Klanten.csv - Regel {i + 1}: Onvoldoende kolommen.");
                    continue;
                }

                Customer customer = new(
                    delen[0],
                    delen[1],
                    delen[2],
                    delen[3],
                    int.Parse(delen[4]),
                    delen[5],
                    delen[6]);

                try
                {
                    using SqlConnection connection = new(DBInfo.ConnectionString);

                    using SqlCommand command = new("Insert into Customers (FirstName, LastName, Email, Street, PostalCode, City, Country) Values (@FirstName, @LastName, @Email, @Street, @PostalCode, @City, @Country)", connection);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Street", customer.StreetName);
                    command.Parameters.AddWithValue("@PostalCode", customer.ZipCode);
                    command.Parameters.AddWithValue("@City", customer.City);
                    command.Parameters.AddWithValue("@Country", customer.Country);

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
