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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<LegoItem> LegoItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BrickStock Files (*.bsx)|*.bsx";
            if (openFileDialog.ShowDialog() == true)
            {
                LegoFileLoader fileLoader = new LegoFileLoader();
                LegoItems = fileLoader.LoadFromFile(openFileDialog.FileName);

                ItemsDataGrid.ItemsSource = LegoItems;
            }
        }

        private void Filter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string nameFilter = FilterByNameTextBox.Text.ToLower();
            string idFilter = FilterByIdTextBox.Text.ToLower();

            var filteredItems = LegoItems.Where(item =>
                (string.IsNullOrEmpty(nameFilter) || item.ItemName.ToLower().StartsWith(nameFilter)) &&
                (string.IsNullOrEmpty(idFilter) || item.ItemID.ToLower().StartsWith(idFilter))
            ).ToList();

            ItemsDataGrid.ItemsSource = filteredItems;
        }
    }
}