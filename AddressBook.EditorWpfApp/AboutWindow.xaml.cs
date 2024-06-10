using System.Windows;

namespace AddressBook.EditorWpfApp
{
    public partial class AboutWindow 
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
