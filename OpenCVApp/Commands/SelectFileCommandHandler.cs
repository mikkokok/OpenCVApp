using System;
using System.Windows.Input;
using Microsoft.Win32;
using OpenCVApp.Properties;

namespace OpenCVApp.Commands
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
                Title = Resources.SelectFileTitle
            };
        }

        public override void Execute(object parameter)
        {
            AppendMessageToView("SelectFileCommandHandler executed");
            var fileDialogResult = _openFileDialog.ShowDialog();
            if (fileDialogResult != null && fileDialogResult == true)
            {
                AppendMessageToView($"Selected file {_openFileDialog.FileName}");
            }
        }
    }
}