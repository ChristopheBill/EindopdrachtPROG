using RentalService.Domain;
using RentalService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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
    // Interaction logic for ReservationSearchWindow.xaml
    public partial class ReservationSearchWindow : Window
    {
        private RentalServiceApplication _rentalServiceApplication;
        public ReservationSearchWindow(RentalServiceApplication rentalServiceApplication)
        {
            InitializeComponent();
            _rentalServiceApplication = rentalServiceApplication;
            cmbCustomers.ItemsSource = rentalServiceApplication.GetCustomers();
            cmbVestiging.ItemsSource = rentalServiceApplication.GetEstablishments();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCustomers.SelectedItem is CustomerDTO customer 
                && cmbVestiging.SelectedItem is EstablishmentDTO establishment
                && dpDatum.SelectedDate is DateTime date)
            {
                List<ReservationDTO> reservations = _rentalServiceApplication.GetReservationsByCustomerIdEstablishmentIdDate(customer.Id, establishment.Id, date);
                dgReservaties.ItemsSource = reservations;
            }
            else
            {
                MessageBox.Show("Gelieve een vestiging, klant en datum te selecteren.");
            }
        }

        private void btnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            if (dgReservaties.SelectedItem is ReservationDTO reservatie)
            {
                var bevestiging = MessageBox.Show("Weet je zeker dat je deze reservatie wil annuleren?", "Bevestig", MessageBoxButton.YesNo);
                if (bevestiging == MessageBoxResult.Yes)
                {
                    _rentalServiceApplication.RemoveReservation(reservatie.Id);
                    MessageBox.Show("Reservatie geannuleerd.");
                    if (dpDatum.SelectedDate is DateTime date)
                    {
                        dgReservaties.ItemsSource = _rentalServiceApplication.GetReservationsByCustomerIdEstablishmentIdDate(cmbCustomers.SelectedItem is CustomerDTO customer ? customer.Id : 0,
                        cmbVestiging.SelectedItem is EstablishmentDTO establishment ? establishment.Id : 0, date);
                    }
                }
                if (bevestiging == MessageBoxResult.No)
                {
                    MessageBox.Show("Reservatie annuleren geannuleerd.");
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst een reservatie.");
            }
            }

        private void cmbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbVestiging.SelectedItem != null && cmbCustomers.SelectedItem != null && dpDatum.SelectedDate != null)
            {
                btnZoek.IsEnabled = true;
                btnAnnuleer.IsEnabled = false;
            }
        }

        private void dpDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbVestiging.SelectedItem != null && cmbCustomers.SelectedItem != null && dpDatum.SelectedDate != null)
            {
                btnZoek.IsEnabled = true;
                btnAnnuleer.IsEnabled = false;
            }
        }

        private void cmbVestiging_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cmbVestiging.SelectedItem != null && cmbCustomers.SelectedItem != null && dpDatum.SelectedDate != null)
            {
                btnZoek.IsEnabled = true;
                btnAnnuleer.IsEnabled = false;
            }
        }

        private void dgReservaties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReservaties.SelectedItem != null)
                btnAnnuleer.IsEnabled = true;
        }
    }
}
