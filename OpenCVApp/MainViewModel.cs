using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenCVApp
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _message;
        private readonly bool _canExecute;
        private SelectedFile _selectedFile;

        public MainViewModel()
        {
            _message = "Welcome to Open CV Application";
            _canExecute = true;
        }

        public string Message
        {
            get => _message;
            set
            {
                _message += "\n";
                _message += value;
                OnPropertyChanged("Message");
            }
        }

        private void OnPropertyChanged(string message)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        private ICommand _searchCommand;
        private ICommand _cancelCommand;
        private ICommand _selectFileCommand;
        private ICommand _selectFolderCommand;


        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new SearchCommandHandler(this, _canExecute));
        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new CancelCommandHandler(this, _canExecute));
        public ICommand SelectFileCommand => _selectFileCommand ?? (_selectFileCommand = new SelectFileCommandHandler(this, _canExecute));
        public ICommand SelectFolderCommand => _selectFolderCommand ?? (_selectFolderCommand = new SelectFolderCommandHandler(this, _canExecute));

    }
}
