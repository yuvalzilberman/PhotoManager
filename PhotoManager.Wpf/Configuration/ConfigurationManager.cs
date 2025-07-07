using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace PhotoManager.Wpf.Configuration
{
    public class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> _instance =
            new(() => new ConfigurationManager());

        public static ConfigurationManager Instance => _instance.Value;

        private AppSettings _settings = null!;
        private static readonly string ConfigFilePath = 
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "AppSettings.json");

        private ConfigurationManager()
        {
            LoadSettings();
        }

        public AppSettings Settings => _settings;

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var jsonString = File.ReadAllText(ConfigFilePath);
                    var deserializedSettings = JsonSerializer.Deserialize<AppSettings>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    _settings = deserializedSettings ?? new AppSettings(); // Ensure _settings is never null
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
                _settings = new AppSettings();
                System.Diagnostics.Debug.WriteLine($"Failed to load configuration: {ex.Message}");
            }
        }

        private void SaveSettings()
        {
            try
            {
                var directory = Path.GetDirectoryName(ConfigFilePath);
                if (!string.IsNullOrEmpty(directory))
                    _ = Directory.CreateDirectory(directory);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(_settings, options);
                File.WriteAllText(ConfigFilePath, jsonString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save configuration: {ex.Message}");
            }
        }

        public void ReloadSettings() => LoadSettings();

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
        public SolidColorBrush WindowBackgroundBrush => GetColorBrush(Settings.Theme.Colors.WindowBackground);
        public SolidColorBrush InputFieldBackgroundBrush => GetColorBrush(Settings.Theme.Colors.InputFieldBackground);
        public SolidColorBrush InputFieldBorderBrush => GetColorBrush(Settings.Theme.Colors.InputFieldBorder);
        public SolidColorBrush PrimaryAccentBrush => GetColorBrush(Settings.Theme.Colors.PrimaryAccent);
        public SolidColorBrush HoverAccentBrush => GetColorBrush(Settings.Theme.Colors.HoverAccent);
        public SolidColorBrush PressedAccentBrush => GetColorBrush(Settings.Theme.Colors.PressedAccent);
        public SolidColorBrush WhiteTextBrush => GetColorBrush(Settings.Theme.Colors.WhiteText);
        public SolidColorBrush LightGrayTextBrush => GetColorBrush(Settings.Theme.Colors.LightGrayText);
        public SolidColorBrush PlaceholderTextBrush => GetColorBrush(Settings.Theme.Colors.PlaceholderText);
        public SolidColorBrush ErrorTextBrush => GetColorBrush(Settings.Theme.Colors.ErrorText);
    }
} 