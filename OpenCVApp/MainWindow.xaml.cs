using System.Windows;

namespace OpenCVApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }
    }
}
