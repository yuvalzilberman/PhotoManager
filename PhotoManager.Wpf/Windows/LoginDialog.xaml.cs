using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;


namespace PhotoManager.Wpf
{
    public partial class LoginDialog : Window
    {
        public LoginDialog()
        {
            var photoService = App.ServiceProvider.GetRequiredService<PhotoManager.Wpf.Services.PhotoService>();
            var vm = new AccountViewModel(photoService);
            DataContext = vm;
            InitializeComponent();
            vm.RequestClose += (s, e) => this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && ReferenceEquals(e.OriginalSource, sender))
            {
                try { this.DragMove(); } catch (InvalidOperationException) { }
            }
        }

        private void SwitchMode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is AccountViewModel vm && vm.SwitchModeCommand.CanExecute(null))
            {
                vm.SwitchModeCommand.Execute(null);
            }
        }
    }
} 