using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenCVApp.FileObjects;

namespace OpenCVApp.Utils
{
    class FolderIterator
    {
        private List<ImageFile> _imageFileList;
        private readonly string _baseFolder;

        public FolderIterator(string folder)
        {
            _baseFolder = folder;
        }

        public async Task<bool> StartIterationTask()
        {
            _imageFileList = await IterateFolderAsync(_baseFolder);
            return true;
        }

        public async Task<List<ImageFile>> IterateFolderAsync(string folder)
        {
            var iteratedFiles = new List<ImageFile>();
            await Task.Run(async () =>
            {
                foreach (var file in Directory.GetFiles(folder))
                {
                    var tempFile = new FileInfo(file);
                    Console.WriteLine($"File found {file}");
                    

                }
                foreach (var directory in Directory.GetDirectories(folder))
                {
                    Console.WriteLine($"Folder found {directory}");
                    iteratedFiles.AddRange(await IterateFolderAsync(directory));
                    
                }
            });


            return iteratedFiles;
        }

    }
}
