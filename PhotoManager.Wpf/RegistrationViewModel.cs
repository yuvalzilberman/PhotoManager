using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.RegularExpressions;
using PhotoManager.Wpf.Resources;

namespace PhotoManager.Wpf
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private readonly RegistrationDialog _dialog;
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

        public RegistrationViewModel()
        {        
            CreateAccountCommand = new RelayCommand(OnCreateAccount);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void OnCreateAccount()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = StringResourceManager.Validation_UsernameRequired;
                return;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = StringResourceManager.Validation_EmailRequired;
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = StringResourceManager.Validation_PasswordRequired;
                return;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = StringResourceManager.Validation_ConfirmPasswordRequired;
                return;
            }

            if (Username.Length < 3)
            {
                ErrorMessage = StringResourceManager.Validation_UsernameMinLength;
                return;
            }

            if (Password.Length < 6)
            {
                ErrorMessage = StringResourceManager.Validation_PasswordMinLength;
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = StringResourceManager.Validation_PasswordsDoNotMatch;
                return;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = StringResourceManager.Validation_EmailInvalid;
                return;
            }

            // TODO: Here you would typically call your backend API to create the user
            // For now, we'll simulate a successful registration
            
            // Registration successful
            IsRegistrationSuccessful = true;
            _dialog.DialogResult = true;
            _dialog.Close();
        }

        private void OnCancel()
        {
            IsRegistrationSuccessful = false;
            _dialog.DialogResult = false;
            _dialog.Close();
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