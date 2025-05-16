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
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : Window
    {
        private DomainManager _domainManager;
        private readonly CustomerDTO _customer;
        public OverviewWindow(DomainManager domainManager, CustomerDTO customer)
        {
            InitializeComponent();
            _domainManager = domainManager;
            _customer = customer;
            inlogGegevens.Text = $"Ingelogd als {customer.ToString()}.";
        }

        private void ReservatieMaken_Click(object sender, RoutedEventArgs e)
        {
            var window = new ReservationCreateWindow(_domainManager, this._customer);
            window.ShowDialog();
        }

        private void ReservatieOpzoeken_Click(object sender, RoutedEventArgs e)
        {
            var window = new ReservationSearchWindow(_domainManager);
            window.ShowDialog();
        }

        private void AutoOverzicht_Click(object sender, RoutedEventArgs e)
        {
            var window = new CarOverviewWindow(_domainManager);
            window.ShowDialog();
        }
    }
}
