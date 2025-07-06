using System.Collections.Generic;

namespace PhotoManager.Wpf.Configuration
{
    public class ThemeConfiguration
    {
        public string Name { get; set; } = "DarkTeal";
        public ColorConfiguration Colors { get; set; } = new();
        public ShadowConfiguration Shadows { get; set; } = new();
    }

    public class ColorConfiguration
    {
        public string WindowBackground { get; set; } = "#2D2D30";
        public string InputFieldBackground { get; set; } = "#3E3E42";
        public string InputFieldBorder { get; set; } = "#505050";
        public string PrimaryAccent { get; set; } = "#20B2AA";
        public string HoverAccent { get; set; } = "#48C9B0";
        public string PressedAccent { get; set; } = "#1A8A8A";
        public string WhiteText { get; set; } = "#FFFFFF";
        public string LightGrayText { get; set; } = "#CCCCCC";
        public string PlaceholderText { get; set; } = "#999999";
        public string ErrorText { get; set; } = "#F14C4C";
    }

    public class ShadowConfiguration
    {
        public ShadowSettings WindowShadow { get; set; } = new() 
        { 
            BlurRadius = 20, 
            Direction = 270, 
            ShadowDepth = 5, 
            Color = "#000000", 
            Opacity = 0.3 
        };
        
        public ShadowSettings ButtonShadow { get; set; } = new() 
        { 
            BlurRadius = 8, 
            Direction = 270, 
            ShadowDepth = 2, 
            Color = "#000000", 
            Opacity = 0.3 
        };
        
        public ShadowSettings IconShadow { get; set; } = new() 
        { 
            BlurRadius = 10, 
            Direction = 270, 
            ShadowDepth = 3, 
            Color = "#000000", 
            Opacity = 0.3 
        };
        
        public ShadowSettings InputFieldShadow { get; set; } = new() 
        { 
            BlurRadius = 4, 
            Direction = 270, 
            ShadowDepth = 1, 
            Color = "#000000", 
            Opacity = 0.2 
        };
    }

    public class ShadowSettings
    {
        public double BlurRadius { get; set; } = 8;
        public double Direction { get; set; } = 270;
        public double ShadowDepth { get; set; } = 2;
        public string Color { get; set; } = "#000000";
        public double Opacity { get; set; } = 0.3;
    }

    public class ApplicationConfiguration
    {
        public string WindowTitle { get; set; } = "Photo Manager";
        public int DefaultWindowWidth { get; set; } = 900;
        public int DefaultWindowHeight { get; set; } = 500;
        public int LoginDialogWidth { get; set; } = 200;
        public int RegistrationDialogWidth { get; set; } = 250;
    }

    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = "Data Source=PhotoManager.db";
        public bool EnableLogging { get; set; } = true;
    }

    public class FeaturesConfiguration
    {
        public bool EnableUserRegistration { get; set; } = true;
        public bool EnablePhotoUpload { get; set; } = true;
        public bool EnablePhotoDownload { get; set; } = true;
        public int MaxFileSizeMB { get; set; } = 50;
        public List<string> AllowedImageFormats { get; set; } = new() { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
    }

    public class AppSettings
    {
        public ThemeConfiguration Theme { get; set; } = new();
        public ApplicationConfiguration Application { get; set; } = new();
        public DatabaseConfiguration Database { get; set; } = new();
        public FeaturesConfiguration Features { get; set; } = new();
    }
} 