using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentalService.DataLoader
{
    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        private DataLoaderApplication _dataLoaderApplication;
        private string _filePath1;
        private string _filePath2;
        private string _filePath3;
       
        public MainWindow(DataLoaderApplication dataLoaderApplication)
        {
            InitializeComponent();
            _dataLoaderApplication = dataLoaderApplication;
        }
        private void UploadFile1_Click(object sender, RoutedEventArgs e)
        {
            _filePath1 = OpenFile();
            File1Text.Text = _filePath1 ?? "";
            // Enable the button to load cars only if a file is selected
            if (!string.IsNullOrEmpty(_filePath1))
            {
                btnCars.IsEnabled = true;
            }
            else
            {
                btnCars.IsEnabled = false;
            }
        }

        private void UploadFile2_Click(object sender, RoutedEventArgs e)
        {
            _filePath2 = OpenFile();
            File2Text.Text = _filePath2 ?? "";
            // Enable the button to load customers only if a file is selected
            if (!string.IsNullOrEmpty(_filePath2))
            {
                btnCustomers.IsEnabled = true;
            }
            else
            {
                btnCustomers.IsEnabled = false;
            }
        }

        private void UploadFile3_Click(object sender, RoutedEventArgs e)
        {
            _filePath3 = OpenFile();
            File3Text.Text = _filePath3 ?? "";
            // Enable the button to load reservations only if a file is selected
            if (!string.IsNullOrEmpty(_filePath3))
            {
                btnSendFiles.IsEnabled = true;
            }
            else
            {
                btnSendFiles.IsEnabled = false;
            }
        }

        private string OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "CSV Files|*.csv|All Files|*.*"
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        private void SendFiles_Click(object sender, RoutedEventArgs e)
        {
            _dataLoaderApplication.InitialiseAllFiles(_filePath1, _filePath2, _filePath3);
            MessageBox.Show("Bestanden zijn geladen, foutlog is beschikbaar.");
        }
    }
}