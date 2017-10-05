using System.Windows.Media.Imaging;
using Emgu.CV;
using OpenCVApp.ViewModels;

namespace OpenCVApp.Windows
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    internal partial class ResultWindow
    {
        internal ResultWindow(IImage image, long score)
        {
            //Width = image.Size.Width;
            //Height = image.Size.Height;
            //Title = $"Result Window - MatchAmount: {score}";
            InitializeComponent();
            //ImageLaatikko.Source = 
            //BitmapSource testi = Utils.ImageConverter.ToBitmapSource(image);
        }

        internal ResultWindow(ResultViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
