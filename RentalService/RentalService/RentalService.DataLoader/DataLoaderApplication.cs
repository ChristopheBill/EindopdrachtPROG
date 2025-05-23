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
        private DomainManager _domainManager;
        private MainWindow _mainWindow;
        public DataLoaderApplication(DomainManager domainManager)
        {
            _domainManager = domainManager;
            _mainWindow = new MainWindow(this);
            _mainWindow.Show();
        }
     
        public void InitialiseAllFiles(string padVestigingen, string padAutos, string padKlanten)
        {
            _domainManager.ReadEstablishments(padVestigingen);
            _domainManager.ReadCustomers(padKlanten);
            _domainManager.ReadCars(padAutos);
        }



        

        
    }
}
