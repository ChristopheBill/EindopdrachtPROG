using RentalService.Domain;
using RentalService.Domain.DTOs;
using RentalService.Domain.Repositories;
using System;
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
    /// <summary>
    /// Interaction logic for ReservationCreateWindow.xaml
    /// </summary>
    public partial class ReservationCreateWindow : Window
    {
        private CustomerDTO _customer;
        //private readonly EstablishmentDTO _establishmentDTO;
        private RentalServiceApplication _rentalServiceApplication;
        public ReservationCreateWindow(RentalServiceApplication rentalServiceApplication, CustomerDTO customer)
        {
            InitializeComponent();
            _rentalServiceApplication = rentalServiceApplication;
            _customer = customer;
            dgAutos.ItemsSource = rentalServiceApplication.GetCars();
            cmbEstablishments.ItemsSource = rentalServiceApplication.GetEstablishments().ToList();

        }

        private void btnZoekAutos_Click(object sender, RoutedEventArgs e)
        {

            //if (cmbVestigingen.SelectedItem is not Vestiging vestiging ||
            //dpStart.SelectedDate is not DateTime startDatum ||
            //!TimeSpan.TryParse(txtStartTijd.Text, out var startTijd) ||
            //dpEinde.SelectedDate is not DateTime eindDatum ||
            //!TimeSpan.TryParse(txtEindeTijd.Text, out var eindTijd))
            //{
            //    MessageBox.Show("Vul alle velden correct in.");
            //    return;
            //}

            //DateTime start = startDatum.Add(startTijd);
            //DateTime einde = eindDatum.Add(eindTijd);

            //if (start <= DateTime.Now || einde <= start || (einde - start).TotalDays < 1)
            //{
            //    MessageBox.Show("De huurperiode is ongeldig.");
            //    return;
            //}

            //int? zitplaatsen = null;
            //if (int.TryParse(txtAantalZitplaatsen.Text, out int aantal))
            //{
            //    zitplaatsen = aantal;
            //}

            //List<Auto> beschikbareAutos = _autoService.GetBeschikbareAutos(vestiging.Id, start, einde, zitplaatsen);
            //dgAutos.ItemsSource = beschikbareAutos;

            //if (beschikbareAutos.Count == 0)
            //{
            //    MessageBox.Show("Geen auto's beschikbaar voor de opgegeven periode.");
            //}
        }

        private void btnReserveer_Click(object sender, RoutedEventArgs e)
        {
            try
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
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Selecteer een startdatum, einddatum, auto en vestiging om te reserveren.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij het maken van de reservatie: " + ex.Message);
            }


            //if (dgAutos.SelectedItem is not Auto geselecteerdeAuto)
            //{
            //    MessageBox.Show("Selecteer een auto om te reserveren.");
            //    return;
            //}

            //DateTime start = dpStart.SelectedDate.Value.Add(TimeSpan.Parse(txtStartTijd.Text));
            //DateTime einde = dpEinde.SelectedDate.Value.Add(TimeSpan.Parse(txtEindeTijd.Text));

            //try
            //{
            //    _reservatieService.VoegReservatieToe(_loggedInCustomer.Id, geselecteerdeAuto.Id, start, einde);
            //    this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Fout bij reservatie: " + ex.Message);
            //}
        }

        private void cmbEstablishments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cmbEstablishments.
        }
    }
}
