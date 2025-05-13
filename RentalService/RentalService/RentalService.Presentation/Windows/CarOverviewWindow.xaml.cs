using RentalService.Domain.Models;
using RentalService.Domain.Repositories;
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
    /// Interaction logic for CarOverviewWindow.xaml
    /// </summary>
    public partial class CarOverviewWindow : Window
    {
        private IEstablishmentRepository _establishmentRepository;
        private ICarRepository _carRepository;
        public CarOverviewWindow()
        {
            InitializeComponent();
            lvAutos.ItemsSource = _carRepository.GetCars();

        //    //_establishmentRepository = establishment;
        //    //_carRepository = carRepository;
        //    LoadVestigingen();
        //}

        //private void LoadVestigingen()
        //{
        //    cmbVestigingen.ItemsSource = _establishmentRepository.GetEstablishments();
        //    cmbVestigingen.DisplayMemberPath = "Luchthaven";
        //}

        //private void btnGenereerMarkdown_Click(object sender, RoutedEventArgs e)
        //{
        //    if (cmbVestigingen.SelectedItem is not Establishment vestiging ||
        //        dpDatum.SelectedDate is not DateTime date ||
        //        !TimeSpan.TryParse(txtTijd.Text, out var tijd))
        //    {
        //        MessageBox.Show("Gelieve een geldige vestiging, datum en tijd in te geven.");
        //        return;
        //    }

        //    DateTime tijdstip = date.Add(tijd);
        //    List<Car> beschikbareAutos = _carRepository.GetCars();
        //    dgAutos.ItemsSource = beschikbareAutos;

        //    //string markdown = MarkdownGenerator.Genereer(vestiging, tijdstip, beschikbareAutos);
        //    //System.IO.File.WriteAllText("auto-overzicht.md", markdown);

        //    //MessageBox.Show("Markdown bestand gegenereerd als 'auto-overzicht.md'");
        }
    }
}
