using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace OpenCVApp.Utils
{
    internal static class ImageConverter
    {
        /// <summary>
        /// Delete a GDI object
        /// </summary>
        /// <param name="o">The poniter to the GDI object to be deleted</param>
        /// <returns></returns>
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);
        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
        public static Mat ResizeImageIfTooBig(Mat imageToCheckAndResize)
        {
            Image<Bgr, Byte> imgOriginal = imageToCheckAndResize.ToImage<Bgr, Byte>();

            if (imgOriginal.Height < 2000 && imgOriginal.Width < 2000) return imageToCheckAndResize;

            var startNumber = 0.9;
            var newHeight = (double)imgOriginal.Height;
            var newWidth = (double)imgOriginal.Width;
            while (newHeight >= 2400 && newWidth >= 2400)
            {
                newHeight = imgOriginal.Height * startNumber;
                newWidth = imgOriginal.Width * startNumber;
                startNumber = startNumber - 0.1;
            }
            var resized = imgOriginal.Resize((int)newWidth, (int)newHeight, Inter.Linear);

            return resized.Mat;
        }
    }
}
