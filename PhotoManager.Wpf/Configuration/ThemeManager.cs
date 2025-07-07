using System.Windows;
using System.Windows.Media;

namespace PhotoManager.Wpf.Configuration
{
    public static class ThemeManager
    {
        public static void ApplyTheme()
        {
            var app = Application.Current;
            if (app == null) return;

            // Load configuration
            var settings = ConfigurationManager.Instance.Settings;

            // Apply color brushes
            ApplyColorBrush(app, "WindowBackgroundBrush", settings.Theme.Colors.WindowBackground);
            ApplyColorBrush(app, "DynamicGrayColorBrush", settings.Theme.Colors.WindowBackground);
            ApplyColorBrush(app, "DynamicInputFieldBackgroundBrush", settings.Theme.Colors.InputFieldBackground);
            ApplyColorBrush(app, "DynamicInputFieldBorderBrush", settings.Theme.Colors.InputFieldBorder);
            ApplyColorBrush(app, "DynamicPrimaryAccentBrush", settings.Theme.Colors.PrimaryAccent);
            ApplyColorBrush(app, "DynamicHoverAccentBrush", settings.Theme.Colors.HoverAccent);
            ApplyColorBrush(app, "DynamicPressedAccentBrush", settings.Theme.Colors.PressedAccent);
            ApplyColorBrush(app, "DynamicWhiteTextBrush", settings.Theme.Colors.WhiteText);
            ApplyColorBrush(app, "DynamicLightGrayTextBrush", settings.Theme.Colors.LightGrayText);
            ApplyColorBrush(app, "DynamicPlaceholderTextBrush", settings.Theme.Colors.PlaceholderText);
            ApplyColorBrush(app, "DynamicErrorTextBrush", settings.Theme.Colors.ErrorText);
        }

        private static void ApplyColorBrush(Application app, string resourceKey, string colorString)
        {
            try
            {
                var color = ConfigurationManager.GetColor(colorString);
                var brush = new SolidColorBrush(color);
                app.Resources[resourceKey] = brush;
            }
            catch
            {
                app.Resources[resourceKey] = new SolidColorBrush(Colors.Gray);
            }
        }

        public static void ReloadTheme()
        {
            ConfigurationManager.Instance.ReloadSettings();
            ApplyTheme();
        }
    }
} 