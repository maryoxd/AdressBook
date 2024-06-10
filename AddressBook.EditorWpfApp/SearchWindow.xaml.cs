using System.IO;
using System.Windows;
using System.Windows.Controls;
using AddressBook.CommonLibrary;
using Microsoft.Win32;

namespace AddressBook.EditorWpfApp
{
    public partial class SearchWindow 
    {
        private int _numberOfEmployeesFound;
        private string? _funkcia;
        private string? _pracovisko;
        private readonly EmployeeList _employeeList;
        public int NumberOfEmployeesFound
        {
            get { return _numberOfEmployeesFound; }
            set { _numberOfEmployeesFound = value; }
        }
        public SearchWindow(EmployeeList employeeList)
        {
            InitializeComponent();
            this._employeeList = employeeList;
            NumberOfEmployeesFound = 0;
            UpdateNumberOfFound();
        }

        private void Search_Button_OnClick(object sender, RoutedEventArgs e)
        {
            string name = NameBlock.Text;
            SearchResult searchResult = _employeeList.Search(_pracovisko, _funkcia, name);
            PeopleListBox.ItemsSource = searchResult.Employees;
            NumberOfEmployeesFound = searchResult.Employees.Length;
            UpdateNumberOfFound();
        }

        private void Pracovisko_ComboBox(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
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

        private void Funkcia_ComboBox(object sender, SelectionChangedEventArgs e)
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
        private void Export_Button_OnClick(object sender, RoutedEventArgs e)
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
        private void Reset_Button_OnClick(object sender, RoutedEventArgs e)
        {
            NameBlock.Text = null!;
            PocetNajdenych.Text = "0";

            if (ComboBoxPositionsFunkcia.SelectedItem != null)
            {
                ComboBoxPositionsFunkcia.SelectedItem = null;
            }

            if (ComboBoxPositionsPracovisko.SelectedItem != null)
            {
                ComboBoxPositionsPracovisko.SelectedItem = null;
            }

            if (PeopleListBox.ItemsSource != null)
            {
                PeopleListBox.ItemsSource = null;
            }
        }
        private void UpdateNumberOfFound()
        {
            PocetNajdenych.Text = $"{NumberOfEmployeesFound}";
        }
    }
}
