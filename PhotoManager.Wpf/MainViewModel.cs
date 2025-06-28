using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PhotoManager.Data.Models;
using PhotoManager.Wpf.Services;


namespace PhotoManager.Wpf
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand BrowseCommand { get; }
        public ICommand UploadCommand { get; }

        private string[] _selectedFilePaths;
        public string[] SelectedFilePaths
        {
            get => _selectedFilePaths;
            set
            {
                _selectedFilePaths = value;
                OnPropertyChanged(nameof(SelectedFilePaths));
            }
        }
        
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
            UploadCommand = new RelayCommand(OnUpload);

            LoadPhotos();
        }

        private async void LoadPhotos()
        {
            var list = await _photoService.GetPhotosAsync();
            Photos = new ObservableCollection<Photo>(list);
        }

        public string SelectedFilePathDisplay =>
            SelectedFilePaths == null ? "" : string.Join("; ", SelectedFilePaths);

        private void OnBrowse()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                SelectedFilePaths = dialog.FileNames;               
                OnPropertyChanged(nameof(SelectedFilePathDisplay));
                CommandManager.InvalidateRequerySuggested(); 
            }
        }

        private async void OnUpload()
        {
            var (success, message) = await _photoService.UploadPhotoAsync(SelectedFilePaths);
            if (!string.IsNullOrEmpty(message))
                MessageBox.Show(message);            
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
