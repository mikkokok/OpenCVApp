using System;
using System.Windows.Input;
using Microsoft.Win32;

namespace OpenCVApp
{
    internal class SelectFileCommandHandler : CommandHandlerBase
    {
        private OpenFileDialog _openFileDialog;
        public SelectFileCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel, canExecute)
        {
            _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false,
                Title = "Open CV Application file selection"
            };
        }

        public override void Execute(object parameter)
        {
            appendMessageToView("SelectFileCommandHandler executed");
            var fileDialogResult = _openFileDialog.ShowDialog();
            if (fileDialogResult != null && fileDialogResult == true)
            {
                appendMessageToView($"Selected file {_openFileDialog.FileName}");
            }
        }
    }
}