using System;
using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace PhotoManager.Wpf.Configuration
{
    public static class ConfigurationManager
    {
        private static AppSettings? _settings;
        private static readonly string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "AppSettings.json");

        public static AppSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    LoadSettings();
                }

                // Ensure _settings is not null by returning a default instance if LoadSettings fails
                return _settings ?? new AppSettings();
            }
        }

        public static void LoadSettings()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var jsonString = File.ReadAllText(ConfigFilePath);
                    _settings = JsonSerializer.Deserialize<AppSettings>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    // Create default settings if file doesn't exist
                    _settings = new AppSettings();
                    SaveSettings();
                }
            }
            catch (Exception ex)
            {
                // Fallback to default settings if loading fails
                _settings = new AppSettings();
                System.Diagnostics.Debug.WriteLine($"Failed to load configuration: {ex.Message}");
            }
        }

        public static void SaveSettings()
        {
            try
            {
                var directory = Path.GetDirectoryName(ConfigFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(_settings, options);
                File.WriteAllText(ConfigFilePath, jsonString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save configuration: {ex.Message}");
            }
        }

        public static void ReloadSettings()
        {
            _settings = null;
            LoadSettings();
        }

        // Helper methods for converting color strings to brushes
        public static SolidColorBrush GetColorBrush(string colorString)
        {
            try
            {
                var color = (Color)ColorConverter.ConvertFromString(colorString);
                return new SolidColorBrush(color);
            }
            catch
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        public static Color GetColor(string colorString)
        {
            try
            {
                return (Color)ColorConverter.ConvertFromString(colorString);
            }
            catch
            {
                return Colors.Black;
            }
        }

        // Theme-specific helper methods
        public static SolidColorBrush WindowBackgroundBrush => GetColorBrush(Settings.Theme.Colors.WindowBackground);
        public static SolidColorBrush InputFieldBackgroundBrush => GetColorBrush(Settings.Theme.Colors.InputFieldBackground);
        public static SolidColorBrush InputFieldBorderBrush => GetColorBrush(Settings.Theme.Colors.InputFieldBorder);
        public static SolidColorBrush PrimaryAccentBrush => GetColorBrush(Settings.Theme.Colors.PrimaryAccent);
        public static SolidColorBrush HoverAccentBrush => GetColorBrush(Settings.Theme.Colors.HoverAccent);
        public static SolidColorBrush PressedAccentBrush => GetColorBrush(Settings.Theme.Colors.PressedAccent);
        public static SolidColorBrush WhiteTextBrush => GetColorBrush(Settings.Theme.Colors.WhiteText);
        public static SolidColorBrush LightGrayTextBrush => GetColorBrush(Settings.Theme.Colors.LightGrayText);
        public static SolidColorBrush PlaceholderTextBrush => GetColorBrush(Settings.Theme.Colors.PlaceholderText);
        public static SolidColorBrush ErrorTextBrush => GetColorBrush(Settings.Theme.Colors.ErrorText);
    }
} 