using System;
using System.Windows.Input;
using OpenCVApp.ViewModels;

namespace OpenCVApp.Commands
{
    internal class DoubleClickCommandHandler : ICommand
    {
        private MainViewModel mainViewModel;
        private bool _canExecute;

        public DoubleClickCommandHandler(MainViewModel mainViewModel, bool canExecute)
        {
            this.mainViewModel = mainViewModel;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Doubleclick");
        }

        public event EventHandler CanExecuteChanged;
    }
}