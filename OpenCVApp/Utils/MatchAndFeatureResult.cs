using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Emgu.CV;

namespace OpenCVApp.Utils
{
    internal class MatchAndFeatureResult
    {
        public Mat Mat { get; }
        public long Score { get; }
        public string Name { get; }
        public string FullName { get; }

        public MatchAndFeatureResult(Mat mat, long score, string name, string fullName)
        {
            Mat = mat;
            Score = score;
            Name = name;
            FullName = fullName;
        }

        public BitmapSource GetImageAsBitmapSource()
        {
            return ImageConverter.ToBitmapSource(Mat);
        }
    }
}
