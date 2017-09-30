using System;
using System.Windows.Forms;
using OpenCVApp.Properties;
using OpenCVApp.Utils;

namespace OpenCVApp.Commands
{
    internal class SelectFolderCommandHandler : CommandHandlerBase
    {
        private OpenFileDialog _openFileDialog;
        private string _selectedFolder;
        public SelectFolderCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel, canExecute)
        {
            InitializeFolderSelectionDialog();
        }

        public override void Execute(object parameter)
        {
            AppendMessageToView("SelectFolderCommandHandler executed");
            var fileDialogResult = _openFileDialog.ShowDialog();
            if (fileDialogResult != DialogResult.OK) return;
            _selectedFolder = System.IO.Path.GetDirectoryName(_openFileDialog.FileName);
            AppendMessageToView($"Selected folder {_selectedFolder}");
            var testi = new FolderIterator(_selectedFolder);
            testi.StartIterationTask();
        }
        private void InitializeFolderSelectionDialog()
        {
            _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false,
                Title = Resources.SelectFolderTitle,
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder selection."
            };
        }
    }
}