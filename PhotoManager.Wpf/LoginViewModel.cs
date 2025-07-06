using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhotoManager.Wpf.Resources;

namespace PhotoManager.Wpf
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly LoginDialog _dialog;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isLoginSuccessful = false;

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

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoginSuccessful
        {
            get => _isLoginSuccessful;
            set
            {
                _isLoginSuccessful = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        public LoginViewModel(LoginDialog dialog)
        {
            _dialog = dialog;
            LoginCommand = new RelayCommand(OnLogin);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void OnLogin()
        {
            // Get password from PasswordBox since it doesn't support binding
            var passwordBox = _dialog.FindName("PasswordBox") as PasswordBox;
            if (passwordBox != null)
            {
                Password = passwordBox.Password;
            }

            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = StringResourceManager.Validation_UsernameRequired;
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = StringResourceManager.Validation_PasswordRequired;
                return;
            }

            if (Username.Length < 3)
            {
                ErrorMessage = StringResourceManager.Validation_UsernameMinLength;
                return;
            }

            if (Password.Length < 3)
            {
                ErrorMessage = StringResourceManager.Validation_PasswordMinLength;
                return;
            }

            // Login successful
            IsLoginSuccessful = true;
            _dialog.DialogResult = true;
            _dialog.Close();
        }

        private void OnCancel()
        {
            IsLoginSuccessful = false;
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 