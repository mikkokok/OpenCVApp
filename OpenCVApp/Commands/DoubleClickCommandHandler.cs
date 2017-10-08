using System;
using System.Windows.Input;
using OpenCVApp.Utils;
using OpenCVApp.ViewModels;
using OpenCVApp.Windows;

namespace OpenCVApp.Commands
{
    internal class DoubleClickCommandHandler : CommandHandlerBase
    {
        private MainViewModel mainViewModel;
        private bool _canExecute;

        public DoubleClickCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel, canExecute)
        {
            this.mainViewModel = mainViewModel;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public override void Execute(object parameter)
        {
            Console.WriteLine("Doubleclick");

            var result = base.MainViewModel.DisplayResult;
            var viewmodel = new ResultViewModel($"Result Window - MatchAmount: {result.Score}", result.GetImageAsBitmapSource());
            var dialog = new ResultWindow(viewmodel);
            dialog.ShowDialog();
        }

        public event EventHandler CanExecuteChanged;
    }
}