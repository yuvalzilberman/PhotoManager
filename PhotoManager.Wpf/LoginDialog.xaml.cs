using System.Windows;
using System.Windows.Input;

namespace PhotoManager.Wpf
{
    public partial class LoginDialog : Window
    {
        public LoginDialog()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && ReferenceEquals(e.OriginalSource, sender))
            {
                try
                {
                    this.DragMove();
                }
                catch (InvalidOperationException)
                {
                    // Ignore exception if window is closing or not in a valid state for DragMove
                }
            }
        }

        private void CreateAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var registrationDialog = new RegistrationDialog();
            var result = registrationDialog.ShowDialog();
            
            if (result == true)
            {
                // User successfully created account, close login dialog
                this.DialogResult = true;
                this.Close();
            }
        }
    }
} 