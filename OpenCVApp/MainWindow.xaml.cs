using System.Windows;

namespace OpenCVApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainViewModel _mainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.Message = "Search Nappia painettu";
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.Message = "Cancel Nappia painettu";
        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.Message = "Cancel Nappia painettu";
        }
        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.Message = "Cancel Nappia painettu";
        }
    }
}
