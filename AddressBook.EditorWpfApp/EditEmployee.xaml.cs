using System.Windows;

namespace AddressBook.EditorWpfApp
{
    public partial class EditEmployeeWindow  
    {
        private bool _isOkayClicked;

        
        public bool IsOkayClicked
        {
            get { return _isOkayClicked; }
            set
            {
                _isOkayClicked = value;
            }
        }

        public EditEmployeeWindow()
        {
            InitializeComponent();
        }

        private void Button_Okay_OnClick(object sender, RoutedEventArgs e)
        {
            _isOkayClicked = true;
            Close();
        }

        private void Button_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
           Close();
        }
    }
}