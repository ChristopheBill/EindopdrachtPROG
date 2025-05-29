using RentalService.Domain;
using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    // Interaction logic for ReservationCreateWindow.xaml
    public partial class ReservationCreateWindow : Window
    {
        private CustomerDTO _customer;
        private RentalServiceApplication _rentalServiceApplication;
        public ReservationCreateWindow(RentalServiceApplication rentalServiceApplication, CustomerDTO customer)
        {
            InitializeComponent();
            _rentalServiceApplication = rentalServiceApplication;
            _customer = customer;
            cmbEstablishments.ItemsSource = rentalServiceApplication.GetEstablishments().ToList();
            
        }

        private void btnZoekAutos_Click(object sender, RoutedEventArgs e)
        {

            if (cmbEstablishments.SelectedItem is EstablishmentDTO establishment
                && dpStart.SelectedDate is DateTime start
                && dpEinde.SelectedDate is DateTime stop
                && txtAantalZitplaatsen.Text is not null)
            {
                int seats = 0;
                if (int.TryParse(txtAantalZitplaatsen.Text, out int aantal) && aantal != 0)
                {
                    seats = aantal;
                dgAutos.ItemsSource = _rentalServiceApplication.GetCarsBySeatsEstablishmentAvailability(establishment.Id, seats, start, stop);
                }
                else
                {
                    MessageBox.Show("Vul een geldig aantal zitplaatsen in.");
                }
            }
        }

        private void btnReserveer_Click(object sender, RoutedEventArgs e)
        {
                EstablishmentDTO establishment = (EstablishmentDTO)cmbEstablishments.SelectedItem;
                CarDTO car = (CarDTO)dgAutos.SelectedItem;
                DateTime startDate = (DateTime)dpStart.SelectedDate;
                DateTime endDate = (DateTime)dpEinde.SelectedDate;
                int establishmentId = establishment.Id;
                int carId = car.Id;
                int customerId = _customer.Id;
                _rentalServiceApplication.MakeReservation(startDate, endDate, customerId, carId, establishmentId);
                MessageBox.Show("Reservatie succesvol opgeslagen.");
                int seats = 0;
                if (int.TryParse(txtAantalZitplaatsen.Text, out int aantal) && aantal != 0)
                {
                    seats = aantal;
                    dgAutos.ItemsSource = _rentalServiceApplication.GetCarsBySeatsEstablishmentAvailability(establishment.Id, seats, startDate, endDate);
                }
                else
                {
                    MessageBox.Show("Vul een geldig aantal zitplaatsen in.");
                }
        }

        private void dpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStart.SelectedDate.HasValue)
            {
                DateTime selectedDate = dpStart.SelectedDate.Value;
                dpEinde.DisplayDateStart = selectedDate.AddDays(1);
            }
            if (cmbEstablishments.SelectedItem != null && dpStart.SelectedDate != null && dpEinde.SelectedDate != null && txtAantalZitplaatsen.Text is int)
            {
                btnZoek.IsEnabled = true;
            }
        }

        private void dpEinde_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEstablishments.SelectedItem != null && dpStart.SelectedDate != null && dpEinde.SelectedDate != null && txtAantalZitplaatsen.Text is int)
            {
                btnZoek.IsEnabled = true;
            }
        }

        private void cmbEstablishments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEstablishments.SelectedItem != null && dpStart.SelectedDate != null && dpEinde.SelectedDate != null && txtAantalZitplaatsen.Text is int)
            {
                btnZoek.IsEnabled = true;
            }
        }

        private void txtAantalZitplaatsen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cmbEstablishments.SelectedItem != null && dpStart.SelectedDate != null && dpEinde.SelectedDate != null && txtAantalZitplaatsen.Text is int && txtAantalZitplaatsen.Text != "0")
            {
                btnZoek.IsEnabled = true;
            }
            else if (cmbEstablishments.SelectedItem != null && dpStart.SelectedDate != null && dpEinde.SelectedDate != null && txtAantalZitplaatsen.Text is int && txtAantalZitplaatsen.Text == "0")
            {
                MessageBox.Show("Vul een geldig aantal zitplaatsen in.");
                btnZoek.IsEnabled = false;
            }
            else
            {
                btnZoek.IsEnabled = true;
            }
        }

        private void dgAutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAutos.SelectedItem != null)
            {
                btnReserveer.IsEnabled = true;
            }
            else
            {
                btnReserveer.IsEnabled = false;
            }
        }
    }
}
