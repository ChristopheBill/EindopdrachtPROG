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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataLoaderApplication _dataLoaderApplication;
        private string _filePath1;
        private string _filePath2;
        private string _filePath3;
        private List<string> _fouten;
        public MainWindow()
        {
            InitializeComponent();
            _dataLoaderApplication = new();
            _fouten = new List<string>();
            //btnSendFiles.IsEnabled = (_filePath1 != null && _filePath2 != null && _filePath3 != null );
        }
        private void UploadFile1_Click(object sender, RoutedEventArgs e)
        {
            _filePath1 = OpenFile();
            File1Text.Text = _filePath1 ?? "";
        }

        private void UploadFile2_Click(object sender, RoutedEventArgs e)
        {
            _filePath2 = OpenFile();
            File2Text.Text = _filePath2 ?? "";
        }

        private void UploadFile3_Click(object sender, RoutedEventArgs e)
        {
            _filePath3 = OpenFile();
            File3Text.Text = _filePath3 ?? "";
            //btnSendFiles.IsEnabled = true;
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
            //try
            //{
                _dataLoaderApplication.InitialiseAllFiles(_filePath1, _filePath2, _filePath3);

            //}
            //catch (Exception ex)
            //{
            //    _fouten.Add(ex.Message);
            //}
            //MessageBox.Show($"Bestanden geladen, er zijn {_fouten.Count()} fouten opgetreden.");

        }
    }
}