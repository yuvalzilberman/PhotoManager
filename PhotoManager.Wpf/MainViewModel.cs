using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using PhotoManager.Data.Models;
using PhotoManager.Wpf.Services;


namespace PhotoManager.Wpf
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly PhotoService _photoService;

        private ObservableCollection<Photo> _photos;
        public ObservableCollection<Photo> Photos
        {
            get => _photos;
            set
            {
                if (_photos != value)
                {
                    _photos = value;
                    NotifyPropertyChanged(nameof(Photos));
                }
            }
        }

        public MainViewModel(PhotoService photoService)
        {
            _photoService = photoService;
            LoadPhotos();
        }

        private async void LoadPhotos()
        {
            var list = await _photoService.GetPhotosAsync();
            Photos = new ObservableCollection<Photo>(list);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
