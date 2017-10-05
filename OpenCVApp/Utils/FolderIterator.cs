using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using OpenCVApp.FileObjects;
using OpenCVApp.Properties;

namespace OpenCVApp.Utils
{
    internal class FolderIterator
    {
        private List<ImageFile> _imageFileList;
        private readonly string _baseFolder;

        public FolderIterator(string folder)
        {
            _baseFolder = folder;
        }

        public async Task<List<ImageFile>> StartIterationTask()
        {
            _imageFileList = await IterateFolderAsync(_baseFolder);
            return _imageFileList;
        }

        private static async Task<List<ImageFile>> IterateFolderAsync(string folder)
        {
            var iteratedFiles = new List<ImageFile>();
            await Task.Run(async () =>
            {
                foreach (var file in Directory.GetFiles(folder))
                {
                    var fileInfo = new FileInfo(file);
                    Console.WriteLine(Resources.FolderIterator_IterateFolderAsync_File_found, file);
                    if (!TestForImage(file))
                        continue;

                    iteratedFiles.Add(new ImageFile(fileInfo.Name, fileInfo.FullName));
                }
                foreach (var directory in Directory.GetDirectories(folder))
                {
                    Console.WriteLine(Resources.FolderIterator_IterateFolderAsync_Folder_found, directory);
                    iteratedFiles.AddRange(await IterateFolderAsync(directory));

                }
            });
            return iteratedFiles;
        }

        private static bool TestForImage(string file)
        {
            try
            {
                var tempImage = Image.FromFile(file);
                tempImage.Dispose();
                return true;
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine(Resources.FolderIterator_TestForImage, file);
                return false;
            }
        }

        public int FileListCount()
        {
            return _imageFileList.Count;
        }
    }
}
