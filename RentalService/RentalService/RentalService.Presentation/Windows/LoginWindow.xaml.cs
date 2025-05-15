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
        private DomainManager _domainManager;
        private OverviewWindow _overviewWindow;
        public CustomerDTO SelectedCustomer { get; private set; }
        public LoginWindow(DomainManager domainManager)
        {
            InitializeComponent();
            _domainManager = domainManager;
            cmbCustomers.ItemsSource = _domainManager.GetCustomers();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCustomers.SelectedItem is CustomerDTO customer)
            {
                SelectedCustomer = customer;
                //DialogResult = true;
                //Close();
                _overviewWindow = new OverviewWindow(_domainManager, customer);
                _overviewWindow.Show();
            }
            else
            {
                MessageBox.Show("Selecteer een klant om verder te gaan.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
