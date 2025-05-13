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
        private string _file3Path;
        public MainWindow()
        {
            InitializeComponent();
            _dataLoaderApplication = new();
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
            _file3Path = OpenFile();
            File3Text.Text = _file3Path ?? "";
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
            try
            {
                _dataLoaderApplication.InitialiseAllFiles(_filePath1, _filePath2, _file3Path);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}