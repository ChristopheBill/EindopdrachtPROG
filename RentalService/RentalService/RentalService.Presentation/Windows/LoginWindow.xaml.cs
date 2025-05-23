using RentalService.Domain;
using RentalService.Domain.DTOs;
using RentalService.Domain.Models;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private RentalServiceApplication _rentalServiceApplication;
        public LoginWindow(RentalServiceApplication rentalServiceApplication)
        {
            InitializeComponent();
            _rentalServiceApplication = rentalServiceApplication;
            cmbCustomers.ItemsSource = _rentalServiceApplication.GetCustomers();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCustomers.SelectedItem is CustomerDTO customer)
            {
                _rentalServiceApplication.TakeToOverviewWindow(this, customer);
            }
        }

        private void cmbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnLogin.IsEnabled = true;
        }
    }
}
