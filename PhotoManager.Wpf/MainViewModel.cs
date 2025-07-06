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
        public ICommand LoginCommand { get; }

        private string[] _selectedFilePaths;
        public string[] SelectedFilePaths
        {
            get => _selectedFilePaths;
            set
            {
                _selectedFilePaths = value;
                OnPropertyChanged(nameof(SelectedFilePaths));
                OnPropertyChanged(nameof(SelectedFilePathDisplay));
            }
        }

        private string _loggedInUsername = "Not logged in";
        public string LoggedInUsername
        {
            get => _loggedInUsername;
            set
            {
                _loggedInUsername = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoggedIn = false;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged();
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

            BrowseCommand = new RelayCommand(OnBrowse, CanExecuteCommands);
            UploadCommand = new RelayCommand(OnUpload, CanExecuteCommands);
            LoginCommand = new RelayCommand(OnLogin);

            Photos = new ObservableCollection<Photo>();
            
            // Show login dialog on startup
            ShowLoginDialog();
        }

        private bool CanExecuteCommands()
        {
            return IsLoggedIn;
        }

        private void ShowLoginDialog()
        {
            var loginDialog = new LoginDialog();
            if (loginDialog.ShowDialog() == true)
            {
                var loginViewModel = (LoginViewModel)loginDialog.DataContext;
                if (loginViewModel.IsLoginSuccessful)
                {
                    LoggedInUsername = loginViewModel.Username;
                    IsLoggedIn = true;
                    LoadPhotos();
                }
                else
                {
                    // If login was cancelled or failed, close the application
                    Application.Current.Shutdown();
                }
            }
            else
            {
                // If login was cancelled, close the application
                Application.Current.Shutdown();
            }
        }

        private void OnLogin()
        {
            ShowLoginDialog();
        }

        private async void LoadPhotos()
        {
            try
            {
                var list = await _photoService.GetPhotosAsync();
                Photos = new ObservableCollection<Photo>(list);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading photos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                CommandManager.InvalidateRequerySuggested(); 
            }
        }

        private async void OnUpload()
        {
            if (SelectedFilePaths == null || SelectedFilePaths.Length == 0)
            {
                MessageBox.Show("Please select files to upload.", "No Files Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var (success, message) = await _photoService.UploadPhotoAsync(SelectedFilePaths);
                if (success)
                {
                    MessageBox.Show("Files uploaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadPhotos(); // Refresh the list
                    SelectedFilePaths = null; // Clear selection
                }
                else
                {
                    MessageBox.Show($"Upload failed: {message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error during upload: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
