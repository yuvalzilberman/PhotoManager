using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PhotoManager.Data.Models;
using PhotoManager.Wpf.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;


namespace PhotoManager.Wpf
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand BrowseCommand { get; }
        public ICommand UploadCommand { get; }

        public string _selectedFilePath;
        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set
            {
                if (_selectedFilePath != value)
                {
                    _selectedFilePath = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanUpload));
                }
            }
        }

        public void NotifyUploadStateChanged()
        {
            OnPropertyChanged(nameof(CanUpload));
        }

        public bool CanUpload => !string.IsNullOrWhiteSpace(SelectedFilePath);


        public bool IsPhotoSelected => !string.IsNullOrEmpty(SelectedFilePath);

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
                    OnPropertyChanged(nameof(Photos));
                }
            }
        }

        public MainViewModel(PhotoService photoService)
        {
            _photoService = photoService;

            BrowseCommand = new RelayCommand(OnBrowse);
            UploadCommand = new RelayCommand(OnUpload, () => IsPhotoSelected);

            LoadPhotos();
        }

        private async void LoadPhotos()
        {
            var list = await _photoService.GetPhotosAsync();
            Photos = new ObservableCollection<Photo>(list);
        }


        private void OnBrowse()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                SelectedFilePath = dialog.FileName;
                OnPropertyChanged(nameof(SelectedFilePath));
                OnPropertyChanged(nameof(IsPhotoSelected));
                CommandManager.InvalidateRequerySuggested(); // refresh button enable state
            }
        }

        private async void OnUpload()
        {
            if (!File.Exists(SelectedFilePath)) return;

            _photoService.UploadPhotoAsync(SelectedFilePath);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
