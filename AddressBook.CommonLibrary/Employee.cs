using System.ComponentModel;

namespace AddressBook.CommonLibrary
{
    public record Employee : INotifyPropertyChanged
    {
        private string? _name;
        private string? _position;
        private string? _phone;
        private string? _email;
        private string? _room;
        private string? _mainWorkPlace;
        private string? _workPlace;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string? Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public string? Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string? Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string? Room
        {
            get { return _room; }
            set
            {
                _room = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        public string? MainWorkPlace
        {
            get { return _mainWorkPlace; }
            set
            {
                _mainWorkPlace = value;
                OnPropertyChanged(nameof(MainWorkPlace));
            }
        }

        public string? WorkPlace
        {
            get { return _workPlace; }
            set
            {
                _workPlace = value;
                OnPropertyChanged(nameof(WorkPlace));
            }
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
