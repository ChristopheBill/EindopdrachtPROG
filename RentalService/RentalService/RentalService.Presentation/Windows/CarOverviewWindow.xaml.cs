using RentalService.Domain;
using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
using RentalService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RentalService.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for CarOverviewWindow.xaml
    /// </summary>
    public partial class CarOverviewWindow : Window
    {
        private RentalServiceApplication _rentalServiceApplication;
        public CarOverviewWindow(RentalServiceApplication rentalServiceApplication)
        {
            InitializeComponent();
            _rentalServiceApplication = rentalServiceApplication;
            //lvAutos.ItemsSource = _rentalServiceApplication.GetCars();
            cmbVestigingen.ItemsSource = _rentalServiceApplication.GetEstablishments();
        }

        private void btnGenereerMarkdown_Click(object sender, RoutedEventArgs e)
        {
           if (lvAutos.SelectedItem is CarDTO car
                && cmbVestigingen.SelectedItem is EstablishmentDTO establishment)
            {
                _rentalServiceApplication.GenerateMarkdown(car.Id, establishment.Id);
                MessageBox.Show("Markdown file is gegenereerd.");
            }
        }

        private void getCarsByEstablishmentId_Click(object sender, RoutedEventArgs e)
        {
            if (cmbVestigingen.SelectedItem is EstablishmentDTO establishment)
            {
                int id = establishment.Id;
                lvAutos.ItemsSource = _rentalServiceApplication.GetCarsByEstablishment(id);
            }
            else
            {
                MessageBox.Show("Gelieve een vestiging te selecteren.");
            }
        }
    }
}
