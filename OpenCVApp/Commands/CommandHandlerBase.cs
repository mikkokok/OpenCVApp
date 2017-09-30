﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenCVApp.Commands
{
    abstract class CommandHandlerBase : ICommand
    {
        private readonly bool _canExecute;
        private readonly MainViewModel _mainViewModel;

        protected CommandHandlerBase(MainViewModel mainViewModel, bool canExecute)
        {
            _mainViewModel = mainViewModel;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        internal void AppendMessageToView(string message)
        {
            _mainViewModel.Message = message;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}
