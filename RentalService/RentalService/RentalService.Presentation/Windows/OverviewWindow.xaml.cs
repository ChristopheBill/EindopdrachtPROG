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
        private RentalServiceApplication _rentalServiceApplication;
        public OverviewWindow(RentalServiceApplication rentalServiceApplication, CustomerDTO customer)
        {
            InitializeComponent();
            _rentalServiceApplication = rentalServiceApplication;
            _customer = customer;
            inlogGegevens.Text = $"Ingelogd als {customer.FirstName} {customer.LastName}.";
        }

        private void ReservatieMaken_Click(object sender, RoutedEventArgs e)
        {
            _rentalServiceApplication.TakeToReservationCreateWindow(this, _customer);
        }

        private void ReservatieOpzoeken_Click(object sender, RoutedEventArgs e)
        {
            _rentalServiceApplication.TakeToReservationSearchWindow(this);
        }

        private void AutoOverzicht_Click(object sender, RoutedEventArgs e)
        {
            _rentalServiceApplication.TakeToCarOverviewWindow(this);
        }
    }
}
