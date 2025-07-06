using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace PhotoManager.Wpf.Configuration
{
    public static class ThemeManager
    {
        public static void ApplyTheme()
        {
            var app = Application.Current;
            if (app == null) return;

            // Load configuration
            var settings = ConfigurationManager.Settings;

            // Apply color brushes
            ApplyColorBrush(app, "DynamicWindowBackgroundBrush", settings.Theme.Colors.WindowBackground);
            ApplyColorBrush(app, "DynamicInputFieldBackgroundBrush", settings.Theme.Colors.InputFieldBackground);
            ApplyColorBrush(app, "DynamicInputFieldBorderBrush", settings.Theme.Colors.InputFieldBorder);
            ApplyColorBrush(app, "DynamicPrimaryAccentBrush", settings.Theme.Colors.PrimaryAccent);
            ApplyColorBrush(app, "DynamicHoverAccentBrush", settings.Theme.Colors.HoverAccent);
            ApplyColorBrush(app, "DynamicPressedAccentBrush", settings.Theme.Colors.PressedAccent);
            ApplyColorBrush(app, "DynamicWhiteTextBrush", settings.Theme.Colors.WhiteText);
            ApplyColorBrush(app, "DynamicLightGrayTextBrush", settings.Theme.Colors.LightGrayText);
            ApplyColorBrush(app, "DynamicPlaceholderTextBrush", settings.Theme.Colors.PlaceholderText);
            ApplyColorBrush(app, "DynamicErrorTextBrush", settings.Theme.Colors.ErrorText);

            // Apply shadow effects
            ApplyShadowEffect(app, "DynamicWindowShadow", settings.Theme.Shadows.WindowShadow);
            ApplyShadowEffect(app, "DynamicButtonShadow", settings.Theme.Shadows.ButtonShadow);
            ApplyShadowEffect(app, "DynamicIconShadow", settings.Theme.Shadows.IconShadow);
            ApplyShadowEffect(app, "DynamicInputFieldShadow", settings.Theme.Shadows.InputFieldShadow);
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
                // Fallback to default if color parsing fails
                app.Resources[resourceKey] = new SolidColorBrush(Colors.Gray);
            }
        }

        private static void ApplyShadowEffect(Application app, string resourceKey, ShadowSettings shadowSettings)
        {
            try
            {
                var color = ConfigurationManager.GetColor(shadowSettings.Color);
                var effect = new DropShadowEffect
                {
                    BlurRadius = shadowSettings.BlurRadius,
                    Direction = shadowSettings.Direction,
                    ShadowDepth = shadowSettings.ShadowDepth,
                    Color = color,
                    Opacity = shadowSettings.Opacity
                };
                app.Resources[resourceKey] = effect;
            }
            catch
            {
                // Fallback to default if shadow creation fails
                app.Resources[resourceKey] = new DropShadowEffect
                {
                    BlurRadius = 8,
                    Direction = 270,
                    ShadowDepth = 2,
                    Color = Colors.Black,
                    Opacity = 0.3
                };
            }
        }

        public static void ReloadTheme()
        {
            ConfigurationManager.ReloadSettings();
            ApplyTheme();
        }
    }
} 