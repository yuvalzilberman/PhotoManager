using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PhotoManager.Wpf.Services;
using PhotoManager.Wpf.Configuration;


namespace PhotoManager.Wpf
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Apply theme from configuration
            ThemeManager.ApplyTheme();

            var services = new ServiceCollection();

            // Register services
            services.AddSingleton<PhotoService>();

            // Register ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<RegistrationViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            // Create MainWindow with ViewModel resolved by DI
            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
            };
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}