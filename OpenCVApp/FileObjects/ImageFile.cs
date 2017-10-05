using System.IO;

namespace OpenCVApp.FileObjects
{
    internal class ImageFile
    {
        public string Name { get; }
        public string FullName { get; set; }

        public ImageFile(string name, string nameWithPath)
        {
            Name = name;
            FullName = nameWithPath;
        }
    }
}