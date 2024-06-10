using System.Windows;
using AddressBook.CommonLibrary;

namespace AddressBook.EditorWpfApp
{
    public partial class EditAddEmployeeWindow 
    {
        public event EventHandler<Employee> EmployeeAdded = null!;
        public EditAddEmployeeWindow()
        {
            InitializeComponent();
        }
        private void Button_Okay_OnClick(object sender, RoutedEventArgs e)
        {
            var newEmployee = new Employee
            {
                Name = TbName.Text,
                Position = TbPosition.Text,
                Phone = TbPhone.Text,
                Email = TbEmail.Text,
                MainWorkPlace = TbMainWorkPlace.Text,
                WorkPlace = TbWorkplace.Text,
                Room = TbRoom.Text
            };

            EmployeeAdded(this, newEmployee);
            DialogResult = true;
            Close();
        }

        private void Button_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
