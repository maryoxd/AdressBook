using System.IO;
using System.Windows;
using System.Windows.Controls;
using AddressBook.CommonLibrary;
using Microsoft.Win32;

namespace AddressBook.ViewerWpfApp
{
    public partial class MainWindow
    {
        private EmployeeList _employeeList = [];
        private int _numberOfEmployeesFound;
        private string? _funkcia;
        private string? _pracovisko;
        public int NumberOfEmployeesFound
        {
            get { return _numberOfEmployeesFound; }
            set { _numberOfEmployeesFound = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            NumberOfFound.Text = "0";
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new()
                {
                    Filter = "JSON files (*.json)|*.json",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    _employeeList = EmployeeList.LoadFromJson(new FileInfo(openFileDialog.FileName))!;
                    FillComboBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CHYBA PRI OTVÁRANÍ SÚBORU: {ex.Message}", "CHYBA", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void FillComboBox()
        {
            var positionsFunkcia = _employeeList.GetPositions();
            foreach (var position in positionsFunkcia)
            {
                ComboBoxItem item = new()
                {
                    Content = position
                };
                ComboBoxPositionsFunkcia.Items.Add(item);
            }


            var positionsPracovisko = _employeeList.GetMainWorkPlaces();
            foreach (var position in positionsPracovisko)
            {
                ComboBoxItem item = new()
                {
                    Content = position
                };
                ComboBoxPositionsPracovisko.Items.Add(item);
            }
        }

        private void ComboBoxPositions_Funkcia_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxPositionsFunkcia.SelectedItem != null)
            {
                _funkcia = (ComboBoxPositionsFunkcia.SelectedItem as ComboBoxItem)?.Content.ToString();
            }
            else
            {
                _funkcia = null;
            }
        }

        private void ComboBoxPositions_Pracovisko_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxPositionsPracovisko.SelectedItem != null)
            {
                _pracovisko = (ComboBoxPositionsPracovisko.SelectedItem as ComboBoxItem)?.Content.ToString();
            }
            else
            {
                _pracovisko = null;
            }
        }

        private void ButtonBase_Search_OnClick(object sender, RoutedEventArgs e)
        {
            string name = NameBlock.Text;
            SearchResult searchResult = _employeeList.Search(_pracovisko, _funkcia, name);
            EmployeeListBox.ItemsSource = searchResult.Employees;
            NumberOfEmployeesFound = searchResult.Employees.Length;
            NumberOfFound.Text = $"{NumberOfEmployeesFound}";
        }
        private void ButtonBase_Reset_OnClick(object sender, RoutedEventArgs e)
        {
            NameBlock.Text = null!;
            NumberOfFound.Text = "0";

            ComboBoxPositionsFunkcia.SelectedItem = null;
            ComboBoxPositionsPracovisko.SelectedItem = null;
            EmployeeListBox.ItemsSource = null;
        }

        private void ButtonBase_Save_CSV_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "CSV files (*.csv)|*.csv",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                SearchResult searchResult = _employeeList.Search(_pracovisko, _funkcia, NameBlock.Text);
                searchResult.SaveToCsv(new FileInfo(saveFileDialog.FileName));
            }
        }
    }
}
    
