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
        private readonly IReservationRepository _reservatieService;
        private readonly IEstablishmentRepository _vestigingService;
        private readonly ICarRepository _autoService;
        private readonly CustomerDTO _loggedInCustomer;
        public ReservationCreateWindow()
        {
            //(CustomerDTO customer,
            //IReservationRepository reservatieService,
            //IEstablishmentRepository vestigingService,
            //ICarRepository autoService)
            InitializeComponent();
            //_loggedInCustomer = customer;
            //_reservatieService = reservatieService;
            //_vestigingService = vestigingService;
            //_autoService = autoService;

            //cmbVestigingen.ItemsSource = _vestigingService.GetAlleVestigingen();
            //cmbVestigingen.DisplayMemberPath = "Luchthaven";
        }

        private void btnZoekAutos_Click(object sender, RoutedEventArgs e)
        {
            //if (cmbVestigingen.SelectedItem is not Vestiging vestiging ||
            //    dpStart.SelectedDate is not DateTime startDatum ||
            //    !TimeSpan.TryParse(txtStartTijd.Text, out var startTijd) ||
            //    dpEinde.SelectedDate is not DateTime eindDatum ||
            //    !TimeSpan.TryParse(txtEindeTijd.Text, out var eindTijd))
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
            //    MessageBox.Show("Reservatie succesvol opgeslagen.");
            //    this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Fout bij reservatie: " + ex.Message);
            //}
        }
    }
}
