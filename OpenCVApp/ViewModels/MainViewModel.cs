using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using OpenCVApp.Commands;
using OpenCVApp.Utils;

namespace OpenCVApp.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _message;
        private readonly bool _canExecute;
        private List<MatchAndFeatureResult> _foundImageFiles;
        private string _selectFolderButtonContent;
        private string _searchButtonContent;
        private ICommand _searchCommand;
        private ICommand _cancelCommand;
        private ICommand _selectFileCommand;
        private ICommand _selectFolderCommand;
        private ICommand _doubleClickCommandHandler;

        public MainViewModel()
        {
            _message = "Welcome to Open CV Application";
            _selectFolderButtonContent = "Select folder";
            _searchButtonContent = "Search";
            _canExecute = true;
        }

        public string Message
        {
            get => _message;
            set
            {
                _message += "\n";
                _message += value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string SelectFolderButtonContent
        {
            get => _selectFolderButtonContent;
            set
            {
                _selectFolderButtonContent = value;
                OnPropertyChanged(nameof(SelectFolderButtonContent));
            }
        }
        public string SearchButtonContent
        {
            get => _searchButtonContent;
            set
            {
                _searchButtonContent = value;
                OnPropertyChanged(nameof(SearchButtonContent));
            }
        }
        public List<MatchAndFeatureResult> FoundImageFiles
        {
            get => _foundImageFiles;
            set
            {
                _foundImageFiles = value;
                OnPropertyChanged(nameof(FoundImageFiles));
            }
        }
        private MatchAndFeatureResult _displayResult;

        public MatchAndFeatureResult DisplayResult
        {
            get => _displayResult;
            set
            {
                _displayResult = value;
                OnPropertyChanged(nameof(DisplayResult));
            }
        }



        private void OnPropertyChanged(string message)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new SearchCommandHandler(this, _canExecute));
        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new CancelCommandHandler(this, _canExecute));
        public ICommand SelectFileCommand => _selectFileCommand ?? (_selectFileCommand = new SelectFileCommandHandler(this, _canExecute));
        public ICommand SelectFolderCommand => _selectFolderCommand ?? (_selectFolderCommand = new SelectFolderCommandHandler(this, _canExecute));
        public ICommand DoubleClickCommand => _doubleClickCommandHandler ?? (_doubleClickCommandHandler = new DoubleClickCommandHandler(this, _canExecute));

    }
}
