using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoManager.Wpf
{
    public partial class RegistrationDialog : Window
    {
        public RegistrationDialog()
        {
            var vm = App.ServiceProvider.GetRequiredService<RegistrationViewModel>();
            DataContext = vm;
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BackToLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
} 