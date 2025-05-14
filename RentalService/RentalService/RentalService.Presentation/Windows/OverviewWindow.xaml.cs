using RentalService.Domain;
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
        public OverviewWindow(DomainManager domainManager)
        {
            InitializeComponent();
            _domainManager = domainManager;
        }

        private void ReservatieMaken_Click(object sender, RoutedEventArgs e)
        {
            var window = new ReservationCreateWindow(_domainManager);
            window.ShowDialog();
        }

        private void ReservatieOpzoeken_Click(object sender, RoutedEventArgs e)
        {
            var window = new ReservationSearchWindow();
            window.ShowDialog();
        }

        private void AutoOverzicht_Click(object sender, RoutedEventArgs e)
        {
            var window = new CarOverviewWindow(_domainManager);
            window.ShowDialog();
        }
    }
}
