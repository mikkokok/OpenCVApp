using System;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using OpenCVApp.FileObjects;
using OpenCVApp.Properties;
using OpenCVApp.ViewModels;

namespace OpenCVApp.Commands
{
    internal class SelectFileCommandHandler : CommandHandlerBase
    {
        private readonly OpenFileDialog _openFileDialog;
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
            CheckSelectedFile(_openFileDialog.FileName);
        }

        private void CheckSelectedFile(string file)
        {
            var fileInfo = new FileInfo(file);
            try
            {
                var tempImage = Image.FromFile(file);
                SetSelectedImageFile(new ImageFile(fileInfo.Name, fileInfo.FullName));
                tempImage.Dispose();
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}