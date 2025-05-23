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
        //private readonly HashSet<string> uniekeEmails = new();
        //private readonly HashSet<string> uniekeNummerplaten = new();
        //private readonly List<string> fouten = new();
        private DomainManager _domainManager;
        private MainWindow _mainWindow;
        private readonly DataLoaderApplication _dataLoaderApplication;

        public DataLoaderApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;
            _mainWindow = new MainWindow(this);
            _mainWindow.Show();
        }
        public void InitialiseAllFiles(string padVestigingen, string padAutos, string padKlanten)
        {
            //fouten.Clear();
            //uniekeEmails.Clear();
            //uniekeNummerplaten.Clear();
            _domainManager.ReadEstablishments(padVestigingen);
            _domainManager.ReadCustomers(padKlanten);
            _domainManager.ReadCars(padAutos);
        }



        

        
    }
}
