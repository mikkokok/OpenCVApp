using System;
using System.Windows.Input;
using OpenCVApp.Utils;
using OpenCVApp.ViewModels;
using OpenCVApp.Windows;

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
            try
            {
                //$"Result Window - MatchAmount: {score}";
                var result = (MatchAndFeatureResult)parameter;
                var viewmodel = new ResultViewModel($"Result Window - MatchAmount: {result.Score}", result.GetImageAsBitmapSource());
                var dialog = new ResultWindow(viewmodel);
                dialog.ShowDialog();
            }
            catch (Exception)
            {
                Console.WriteLine("Casting failed");                
            }

        }

        public event EventHandler CanExecuteChanged;
    }
}