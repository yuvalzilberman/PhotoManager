using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Text.RegularExpressions;
using PhotoManager.Wpf.Resources;
using PhotoManager.Wpf.Services;
using PhotoManager.Data.Models;

namespace PhotoManager.Wpf
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly PhotoService _photoService;
        public event EventHandler RequestClose;

        public string DialogTitle => IsLoginMode ? StringResourceManager.Login_Title : StringResourceManager.Registration_Title;
        public string DialogSubtitle => IsLoginMode ? StringResourceManager.Login_Subtitle : StringResourceManager.Registration_Subtitle;
        public string PrimaryButtonText => IsLoginMode ? StringResourceManager.Login_SignInButton : StringResourceManager.Registration_CreateAccountButton;
        public string SwitchModeText => IsLoginMode ? StringResourceManager.Login_CreateAccountLink : StringResourceManager.Registration_SignInLink;

        public ICommand PrimaryCommand => IsLoginMode ? LoginCommand : RegisterCommand;
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SwitchModeCommand { get; }

        private string _username = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _statusMessage = string.Empty;
        private Status _status = Status.Valid;
        private bool _isLoginMode = true;
        private bool _isOperationSuccessful = false;

        public string Username
        {
            get => _username;
            set
            { 
                _username = value;
                OnPropertyChanged();
                ClearError(); 
                RaiseCanExecuteChanged();
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
                RaiseCanExecuteChanged();
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
                RaiseCanExecuteChanged(); 
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
                RaiseCanExecuteChanged(); 
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(); 
            }
        }

        public Status Status
        {
            get => _status;
            set 
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoginMode
        {
            get => _isLoginMode;
            set
            {
                _isLoginMode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DialogTitle));
                OnPropertyChanged(nameof(DialogSubtitle)); 
                OnPropertyChanged(nameof(PrimaryButtonText));
                OnPropertyChanged(nameof(SwitchModeText));
                OnPropertyChanged(nameof(PrimaryCommand)); 
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public bool IsOperationSuccessful
        {
            get => _isOperationSuccessful;
            set 
            {
                _isOperationSuccessful = value;
                OnPropertyChanged();
            }
        }

        public AccountViewModel(PhotoService photoService)
        {
            _photoService = photoService;
            LoginCommand = new RelayCommand(async () => await OnLogin(), CanLogin);
            RegisterCommand = new RelayCommand(async () => await OnRegister(), CanRegister);
            CancelCommand = new RelayCommand(OnCancel);
            SwitchModeCommand = new RelayCommand(OnSwitchMode);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                !string.IsNullOrWhiteSpace(Password);
        }

        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(ConfirmPassword)
                && IsValidEmail(Email);
        }

        private async Task OnLogin()
        {
            if (!ValidateLogin()) return;

            var(success, message, user) =  await _photoService.GetUserAsync(Username, Password);
            if (!success)
            {
                StatusMessage = message;
                Status = Status.InValid;
                return;
            }
            IsOperationSuccessful = true;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private async Task OnRegister()
        {
            if (!ValidateRegisteration()) return;

            var addUser = new AppUser(Username, Password, Email);
            
            try
            {
                var (success, message) = await _photoService.AddUserAsync(addUser);
                if (success)
                {
                    IsOperationSuccessful = true;
                    RequestClose?.Invoke(this, EventArgs.Empty);
                    StatusMessage = StringResourceManager.Registration_UserAdded;
                    Status = Status.Valid;
                }
                else
                {
                    StatusMessage = message;
                    Status = Status.InValid;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Exception: {ex.Message}";
                Status = Status.InValid;
            }
        }

        private bool ValidateLogin()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                StatusMessage = StringResourceManager.Validation_UsernameRequired;
                Status = Status.InValid;
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                StatusMessage = StringResourceManager.Validation_PasswordRequired;
                Status = Status.InValid;
                return false;
            }
            return true;
        }

        private bool ValidateRegisteration()
        {
            if (!IsValidEmail(Email))
            {
                StatusMessage = StringResourceManager.Validation_EmailInvalid;
                Status = Status.InValid;
                return false;
            }
            if (!Password.Equals(ConfirmPassword))
            {
                StatusMessage = StringResourceManager.Validation_PasswordsDoNotMatch;
                Status = Status.InValid;
                return false;
            }
            return true;
        }        

        private void OnCancel()
        {
            IsOperationSuccessful = false;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void OnSwitchMode()
        {
            IsLoginMode = !IsLoginMode;
            StatusMessage = string.Empty;
            Status = Status.Valid;
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }

        private void ClearError()
        {
            if (!string.IsNullOrEmpty(StatusMessage))
            {
                StatusMessage = string.Empty;
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
        private void RaiseCanExecuteChanged()
        {
            if (LoginCommand is RelayCommand loginCmd) loginCmd.RaiseCanExecuteChanged();
            if (RegisterCommand is RelayCommand regCmd) regCmd.RaiseCanExecuteChanged();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}