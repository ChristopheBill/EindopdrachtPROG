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
    public class EstablishmentMapper : ILocationRepository
    {
        private readonly List<string> fouten = new();

        public void ReadEstablishments(string pad)
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
    }
}
