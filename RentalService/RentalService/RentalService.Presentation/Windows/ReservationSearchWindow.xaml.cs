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
    /// <summary>
    /// Interaction logic for ReservationSearchWindow.xaml
    /// </summary>
    public partial class ReservationSearchWindow : Window
    {
        private RentalServiceApplication _rentalServiceApplication;
        public ReservationSearchWindow(RentalServiceApplication rentalServiceApplication)
        {
            InitializeComponent();
            _rentalServiceApplication = rentalServiceApplication;
            cmbCustomers.ItemsSource = rentalServiceApplication.GetCustomers();
            cmbVestiging.ItemsSource = rentalServiceApplication.GetEstablishments();
            //dgReservaties.ItemsSource = rentalServiceApplication.GetReservations();
        }

        private void btnZoek_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCustomers.SelectedItem is CustomerDTO customer && cmbVestiging.SelectedItem is EstablishmentDTO establishment)
            {
                List<ReservationDTO> reservations = _rentalServiceApplication.GetReservationsByCustomerIdEstablishmentId(customer.Id, establishment.Id);
                dgReservaties.ItemsSource = reservations;
            }
            else
            {
                MessageBox.Show("Gelieve een vestiging en klant te selecteren.");
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
                    dgReservaties.ItemsSource = _rentalServiceApplication.GetReservationsByCustomerIdEstablishmentId(cmbCustomers.SelectedItem is CustomerDTO customer ? customer.Id : 0,
                        cmbVestiging.SelectedItem is EstablishmentDTO establishment ? establishment.Id : 0);
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
                //    MessageBox.Show("Selecteer eerst een reservatie.");
                //    return;
                //}

                //var bevestiging = MessageBox.Show("Weet je zeker dat je deze reservatie wil annuleren?",
                //                                  "Bevestig", MessageBoxButton.YesNo);
                //if (bevestiging == MessageBoxResult.Yes)
                //{
                //    try
                //    {
                //        _reservatieService.VerwijderReservatie(reservatie.Id);
                //        MessageBox.Show("Reservatie geannuleerd.");
                //        btnZoek_Click(null, null); // herladen
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show("Fout bij annuleren: " + ex.Message);
                //    }
                //}
            }
        }
    }
