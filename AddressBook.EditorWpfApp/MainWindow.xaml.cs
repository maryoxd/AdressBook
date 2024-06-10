using System.Windows;
using System.Windows.Controls;
using AddressBook.CommonLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;

/* S touto triedou mi v niektorých oblastiach pomáhala AI */

namespace AddressBook.EditorWpfApp
{
    public partial class MainWindow 
    {
        private EmployeeList _employeeList = [];
        private int _numberOfEmployeesFound;
        private string _originalJson;
        public int NumberOfEmployeesFound
        {
            get { return _numberOfEmployeesFound; }
            set { _numberOfEmployeesFound = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
            NumberOfEmployeesFound = 0;
            UpdateNumberOfFound();
            _originalJson = JsonConvert.SerializeObject(_employeeList, Formatting.Indented);
        }

        private void MenuItem_OnClick_Open_Employees(object sender, RoutedEventArgs e)
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
                    _employeeList = EmployeeList.LoadFromJson(new(openFileDialog.FileName))!;
                    SearchResult search = _employeeList.Search();
                    EmployeesList.ItemsSource = search.Employees;
                    NumberOfEmployeesFound = search.Employees.Length;
                    UpdateNumberOfFound();
                    UpdateSearchButton();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CHYBA PRI OTVÁRANÍ SÚBORU: {ex.Message}", "CHYBA", MessageBoxButton.OK,
                                       MessageBoxImage.Error);
            }
        }

        private void MenuItem_OnClick_Save_File(object? sender, RoutedEventArgs? e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "JSON files (*.json)|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _employeeList.SaveToJson(new(saveFileDialog.FileName));
            }

            
        }

        private void MenuItem_OnClick_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_OnClick_New_File(object sender, RoutedEventArgs e)
        {
            string currentJson = JsonConvert.SerializeObject(_employeeList, Formatting.Indented);

            if (currentJson != _originalJson)
            {
                MessageBoxResult result = MessageBox.Show("Adresár bol zmenený. Chcete ho uložiť?", "Uložiť adresár", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MenuItem_OnClick_Save_File(null, null);
                }
            }
            _employeeList.Clear();
            NumberOfEmployeesFound = _employeeList.Search().Employees.Length;
            _originalJson = JsonConvert.SerializeObject(_employeeList, Formatting.Indented); 
            EmployeesList.ItemsSource = null;
            UpdateNumberOfFound();
            ManageButtons(true);
            UpdateSearchButton();

        }

        private void ShowAddEmployeeWindow(object sender, RoutedEventArgs e)
        {
            EditAddEmployeeWindow addWindow = new();
            addWindow.EmployeeAdded += EditWindow_EmployeeAdded;
            addWindow.ShowDialog();
        }

        private void EditWindow_EmployeeAdded(object? sender, Employee e)
        {
            _employeeList.Add(e);
            SearchResult search = _employeeList.Search();
            EmployeesList.ItemsSource = search.Employees;
            NumberOfEmployeesFound = search.Employees.Length;
            UpdateNumberOfFound();
            EmployeesList.Items.Refresh();
            UpdateSearchButton();
        }

        private void EmployeesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ManageButtons(false);
        }

        private void Edit_Button_OnClick(object sender, RoutedEventArgs e)
        {
            Employee employee = (Employee)EmployeesList.SelectedItem;

            EditEmployeeWindow editWindow = new();

            editWindow.TbName.Text = employee.Name ?? "";
            editWindow.TbEmail.Text = employee.Email ?? "";
            editWindow.TbPhone.Text = employee.Phone ?? "";
            editWindow.TbRoom.Text = employee.Room ?? "";
            editWindow.TbWorkplace.Text = employee.WorkPlace ?? "";
            editWindow.TbMainWorkPlace.Text = employee.MainWorkPlace ?? "";
            editWindow.TbPosition.Text = employee.Position ?? "";

            editWindow.ShowDialog();

            if (editWindow.IsOkayClicked)
            {
                EmployeesList.SelectedItem = null;
                employee.Name = editWindow.TbName.Text;
                employee.Email = editWindow.TbEmail.Text;
                employee.Phone = editWindow.TbPhone.Text;
                employee.Room = editWindow.TbRoom.Text;
                employee.WorkPlace = editWindow.TbWorkplace.Text;
                employee.MainWorkPlace = editWindow.TbMainWorkPlace.Text;
                employee.Position = editWindow.TbPosition.Text;

                EmployeesList.Items.Refresh();
            }
        }

        private void Item_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Chcete odstrániť vybraného zamestnanca?","Odstrániť zamestnanca", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _employeeList.Remove((Employee)EmployeesList.SelectedItem);
                SearchResult search = _employeeList.Search();
                EmployeesList.ItemsSource = search.Employees;
                NumberOfEmployeesFound = search.Employees.Length;
                EmployeesList.Items.Refresh();
                UpdateNumberOfFound();
                UpdateSearchButton();
                ManageButtons(true);
            }
        }
        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new();
            aboutWindow.Show();
        }
        private void Search_Button_OnClick(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new(_employeeList);
            FillComboBox(searchWindow);
            searchWindow.Show();
        }
        private void FillComboBox(SearchWindow searhSearchWindow)
        {
            var positionsFunkcia = _employeeList.GetPositions();
            foreach (var position in positionsFunkcia)
            {
                ComboBoxItem item = new()
                {
                    Content = position
                };
                searhSearchWindow.ComboBoxPositionsFunkcia.Items.Add(item);
            }

            var positionsPracovisko = _employeeList.GetMainWorkPlaces();
            foreach (var position in positionsPracovisko)
            {
                ComboBoxItem item = new()
                {
                    Content = position
                };
                searhSearchWindow.ComboBoxPositionsPracovisko.Items.Add(item);
            }
        }

        private void UpdateSearchButton()
        {
            SearchButton.IsEnabled = EmployeesList.Items.Count > 0;
        }

        private void ManageButtons(bool shut)
        {
            if (shut)
            {
                EditButton.IsEnabled = false;
                MainEditButton.IsEnabled = false;
                RemoveButton.IsEnabled = false;
                MainRemoveButton.IsEnabled = false;
            }
            else
            {
                EditButton.IsEnabled = true;
                MainEditButton.IsEnabled = true;
                RemoveButton.IsEnabled = true;
                MainRemoveButton.IsEnabled = true;
            }
        }

        private void UpdateNumberOfFound()
        {
            PocetNajdenych.Content = $"{NumberOfEmployeesFound}";
        }
    }
}