using Microsoft.Data.SqlClient;
using RentalService.Domain.Models;
using RentalService.Persistence;
using RentalService.Persistence.Mappers;
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
        private CarMapper _carMapper = new();
        private EstablishmentMapper _establishmentMapper = new();
        private CustomerMapper _customerMapper = new();

        public List<string> InitialiseAllFiles(string padVestigingen, string padAutos, string padKlanten)
        {
            fouten.Clear();
            uniekeEmails.Clear();
            uniekeNummerplaten.Clear();
            _establishmentMapper.ReadEstablishments(padVestigingen);
            _carMapper.ReadCars(padAutos);
            _customerMapper.ReadCustomers(padKlanten);

            return fouten;
        }



        

        
    }
}
