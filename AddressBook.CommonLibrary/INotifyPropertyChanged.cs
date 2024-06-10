using System.ComponentModel;

namespace AddressBook.CommonLibrary
{
    public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}
