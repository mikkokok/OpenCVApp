using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Emgu.CV;
using OpenCVApp.Annotations;

namespace OpenCVApp.ViewModels
{
    internal class ResultViewModel : INotifyPropertyChanged
    {
        private string _title;
        private BitmapSource _resultImage;

        public ResultViewModel(string title, BitmapSource resultImage)
        {
            _title = title;
            _resultImage = resultImage;
        }

        public string Title
        {
            get => _title;
            set
            {
                _title += value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public BitmapSource ResultImage
        {
            get => _resultImage;
            set
            {
                _resultImage = value;
                OnPropertyChanged(nameof(ResultImage));
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
