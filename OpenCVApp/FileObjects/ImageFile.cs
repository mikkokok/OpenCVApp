namespace OpenCVApp.FileObjects
{
    internal class ImageFile : System.IO.FileSystemInfo
    {
        public override string Name { get; }
        public override bool Exists { get; }

        public bool IsPicture { get;}

        public ImageFile(bool isPic, string name)
        {
            IsPicture = isPic;
            Name = name;
            Exists = true;
        }

        public override void Delete()
        {
            
            
        }


    }
}