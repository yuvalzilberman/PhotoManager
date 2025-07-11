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

            vm.RequestClose += (s, e) => this.Close();
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
                }
            }
        }

        private void BackToLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
} 