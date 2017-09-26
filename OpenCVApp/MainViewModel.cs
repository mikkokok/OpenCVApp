using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVApp
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _message;

        public MainViewModel()
        {
            _message = "Welcome to Open CV Application";
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
    }
}
