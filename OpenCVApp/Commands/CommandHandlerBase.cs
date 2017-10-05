using System;
using System.Collections.Generic;
using System.Windows.Input;
using OpenCVApp.FileObjects;
using OpenCVApp.ViewModels;

namespace OpenCVApp.Commands
{
    internal abstract class CommandHandlerBase : ICommand
    {
        private readonly bool _canExecute;
        protected readonly MainViewModel MainViewModel;
        private static ImageFile _selectedImageFile;
        private static List<ImageFile> _foundImageFiles;

        protected CommandHandlerBase(MainViewModel mainViewModel, bool canExecute)
        {
            MainViewModel = mainViewModel;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        internal void AppendMessageToView(string message)
        {
            MainViewModel.Message = message;
        }

        internal void SetFoundImageFiles(List<ImageFile> foundImageFiles)
        {
            _foundImageFiles = foundImageFiles;
        }

        internal List<ImageFile> GetFoundImageFiles()
        {
            return _foundImageFiles;
        }

        internal void SetSelectedImageFile(ImageFile file)
        {
            _selectedImageFile = file;
        }

        internal ImageFile GetSelectedImageFile()
        {
            return _selectedImageFile;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}
