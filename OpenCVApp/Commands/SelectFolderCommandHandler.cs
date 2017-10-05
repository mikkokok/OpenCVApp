using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;
using OpenCVApp.Properties;
using OpenCVApp.Utils;
using OpenCVApp.ViewModels;

namespace OpenCVApp.Commands
{
    internal class SelectFolderCommandHandler : CommandHandlerBase
    {
        private OpenFileDialog _openFileDialog;
        private string _selectedFolder;
        public static bool _canExecute = true;


        public SelectFolderCommandHandler(MainViewModel mainViewModel, bool canExecute) : base(mainViewModel, canExecute)
        {
            InitializeFolderSelectionDialog();
        }

        public override void Execute(object parameter)
        {
            if (!_canExecute) return;

            DisableExecution();

            AppendMessageToView("SelectFolderCommandHandler executed");

            var fileDialogResult = _openFileDialog.ShowDialog();
            if (fileDialogResult != DialogResult.OK) return;

            _selectedFolder = System.IO.Path.GetDirectoryName(_openFileDialog.FileName);
            AppendMessageToView($"Selected folder {_selectedFolder}");

            IterateFolderAndReportToViewModelAsync();
        }

        private async Task IterateFolderAndReportToViewModelAsync()
        {
            UpdateButtonContentWhenSearchingAsync();
            var folderIterator = new FolderIterator(_selectedFolder);
            SetFoundImageFiles(await folderIterator.StartIterationTask());
            AppendMessageToView($"{folderIterator.FileListCount()} images found from {_selectedFolder}");
            EnableExecution();
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

        public static void DisableExecution()
        {
            _canExecute = false;
        }

        public static void EnableExecution()
        {
            _canExecute = true;
        }

        public async Task UpdateButtonContentWhenSearchingAsync()
        {
            await Task.Run(() =>
            {
                while (!_canExecute)
                {
                    MainViewModel.SelectFolderButtonContent = "Loading...";
                    Thread.Sleep(500);
                    MainViewModel.SelectFolderButtonContent = "Loading..";
                    Thread.Sleep(500);
                    MainViewModel.SelectFolderButtonContent = "Loading.";
                    Thread.Sleep(500);
                }
                MainViewModel.SelectFolderButtonContent = "Select folder";
            });
        }
    }
}