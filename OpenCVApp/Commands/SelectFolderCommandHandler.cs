using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace OpenCVApp
{
    internal class SelectFolderCommandHandler : CommandHandlerBase
    {
        //private FolderBrowserDialog _folderBrowserDialog;
        private readonly OpenFileDialog _openFileDialog;
        public SelectFolderCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel, canExecute)
        {
            _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false,
                Title = "Open CV Application folder selection",
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder selection."
            };
            //_folderBrowserDialog = new FolderBrowserDialog
            //{
            //    Description = "Open CV Application folder selection",
            //    RootFolder = Environment.SpecialFolder.MyDocuments
            //};
        }

        public override void Execute(object parameter)
        {
            appendMessageToView("SelectFolderCommandHandler executed");
            var fileDialogResult =  _openFileDialog.ShowDialog();
            if (fileDialogResult == DialogResult.OK)
            {
                appendMessageToView($"Selected file {_openFileDialog.FileName}");
            }
            //var folderBrowserDialogResult = _folderBrowserDialog.ShowDialog();
            //if (folderBrowserDialogResult == DialogResult.OK)
            //{
            //    appendMessageToView($"Selected folder is {_folderBrowserDialog.SelectedPath}");
            //}
        }
    }
}