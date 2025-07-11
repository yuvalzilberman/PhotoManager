using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.RegularExpressions;
using PhotoManager.Wpf.Resources;
using PhotoManager.Wpf.Services;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using PhotoManager.Data.Models;

namespace PhotoManager.Wpf
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public event EventHandler RequestClose;
        private readonly PhotoService _photoService;
        private string _username = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isRegistrationSuccessful = false;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                ClearError();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                ClearError();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ClearError();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                ClearError();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsRegistrationSuccessful
        {
            get => _isRegistrationSuccessful;
            set
            {
                _isRegistrationSuccessful = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateAccountCommand { get; }
        public ICommand CancelCommand { get; }

        public RegistrationViewModel(PhotoService photoService)
        {
            _photoService = photoService;
            CreateAccountCommand = new RelayCommand(async () => await OnCreateAccount(), OnCreateAccountCanExecute);
            CancelCommand = new RelayCommand(OnCancel);

            //var isConnected = await _photoService.TestConnectionAsync(); // Await the Task<bool> to get the result  
            //if (!isConnected)
            //{
            //    ErrorMessage = "Cannot connect to the server. Please check if the API is running.";
            //    return;
            //}
        }

        private bool OnCreateAccountCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                !string.IsNullOrWhiteSpace(Password) &&
                !string.IsNullOrWhiteSpace(Email) &&
                !string.IsNullOrWhiteSpace(ConfirmPassword);
        } 

        private async Task OnCreateAccount()
        {
            if (!IsValidEmail(Email))
            {
                ErrorMessage = StringResourceManager.Validation_EmailInvalid;
                return;
            }

            if (!Password.Equals(ConfirmPassword))
            {
                ErrorMessage = StringResourceManager.Validation_PasswordsDoNotMatch;
                return;
            }

            var addUser = new AppUser
            {
                UserName = Username,
                Email = Email,
                Password = Password,
                CreatedAt = DateTime.UtcNow
            };    
                       
            try
            {
                var (success, message) = await _photoService.AddUserAsync(addUser);
                if (success)
                {
                    IsRegistrationSuccessful = true;
                    RequestClose?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    ErrorMessage = message;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Exception: {ex.Message}";
            }
        }

        private void OnCancel()
        {
            IsRegistrationSuccessful = false;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ClearError()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = string.Empty;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 