using Microsoft.Data.SqlClient;
using RentalService.Domain;
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
        //private DomainManager _domainManager;

        public void InitialiseAllFiles(string padVestigingen, string padAutos, string padKlanten)
        {
            fouten.Clear();
            uniekeEmails.Clear();
            uniekeNummerplaten.Clear();
            //string? path = Path.GetDirectoryName(padAutos) ?? throw new Exception("Folder path is null.");
            //string errorlogPath = Path.Combine(path, "ErrorLog.txt");
            //if (File.Exists(errorlogPath))
            //{
            //    File.Delete(errorlogPath);
            //}
            //try
            //{
                _customerMapper.ReadCustomers(padKlanten);
                _establishmentMapper.ReadEstablishments(padVestigingen);
                _carMapper.ReadCars(padAutos);
            //}
            //catch (Exception ex)
            //{
            //    fouten.Add(ex.Message);
            //}
            //if (fouten.Count > 0)
            //{
            //    using StreamWriter writer = new(errorlogPath, true);
            //    foreach (string fout in fouten)
            //    {
            //        writer.WriteLine(fout);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Alle bestanden zijn succesvol ingelezen.");
            //}

            //return fouten;
        }



        

        
    }
}
