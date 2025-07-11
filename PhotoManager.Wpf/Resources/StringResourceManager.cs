using System.Resources;

namespace PhotoManager.Wpf.Resources
{
    public static class StringResourceManager
    {
        private static readonly ResourceManager ResourceManager = new ResourceManager("PhotoManager.Wpf.Resources.StringResources", typeof(StringResourceManager).Assembly);

        // Login Dialog Strings
        public static string Login_Welcome => ResourceManager.GetString("Login_Welcome") ?? "Welcome";
        public static string Login_Subtitle => ResourceManager.GetString("Login_Subtitle") ?? "Sign in to your existing account";
        public static string Login_UsernamePlaceholder => ResourceManager.GetString("Login_UsernamePlaceholder") ?? "User name";
        public static string Login_PasswordPlaceholder => ResourceManager.GetString("Login_PasswordPlaceholder") ?? "Password";
        public static string Login_SignInButton => ResourceManager.GetString("Login_SignInButton") ?? "Sign In";
        public static string Login_CancelButton => ResourceManager.GetString("Login_CancelButton") ?? "Cancel";
        public static string Login_CreateAccountLink => ResourceManager.GetString("Login_CreateAccountLink") ?? "Don't have an account? Create one";
        public static string Login_EmailPlaceholder => ResourceManager.GetString("Login_EmailPlaceholder") ?? "Email";
        public static string Login_ConfirmPasswordPlaceholder => ResourceManager.GetString("Login_ConfirmPasswordPlaceholder") ?? "Confirm Password";
        public static string Login_Title => ResourceManager.GetString("Login_Title") ?? "Login";

        // Registration Dialog Strings
        public static string Registration_Title => ResourceManager.GetString("Registration_Title") ?? "Create Account";
        public static string Registration_Subtitle => ResourceManager.GetString("Registration_Subtitle") ?? "Enter your details to create a new account";
        public static string Registration_UsernamePlaceholder => ResourceManager.GetString("Registration_UsernamePlaceholder") ?? "Username";
        public static string Registration_EmailPlaceholder => ResourceManager.GetString("Registration_EmailPlaceholder") ?? "Email";
        public static string Registration_PasswordPlaceholder => ResourceManager.GetString("Registration_PasswordPlaceholder") ?? "Password";
        public static string Registration_ConfirmPasswordPlaceholder => ResourceManager.GetString("Registration_ConfirmPasswordPlaceholder") ?? "Confirm Password";
        public static string Registration_CreateAccountButton => ResourceManager.GetString("Registration_CreateAccountButton") ?? "Create Account";
        public static string Registration_BackToLoginLink => ResourceManager.GetString("Registration_BackToLoginLink") ?? "Already have an account? Sign in";
        public static string Registration_SignInLink => ResourceManager.GetString("Registration_SignInLink") ?? "Already have an account? Sign in";
        public static string Registration_AddUser => ResourceManager.GetString("RegistrationViewModel_AddedUser") ?? "";
        public static string Registration_AddUserFailure => ResourceManager.GetString("RegistrationViewModel_AddedUserFailure") ?? "";
        public static string Registration_UserAdded => ResourceManager.GetString("RegistrationViewModel_UserAdded") ?? "";


        // Validation Messages
        public static string Validation_UsernameRequired => ResourceManager.GetString("Validation_UsernameRequired") ?? "Please enter a username.";
        public static string Validation_UsernameMinLength => ResourceManager.GetString("Validation_UsernameMinLength") ?? "Username must be at least 3 characters long.";
        public static string Validation_EmailRequired => ResourceManager.GetString("Validation_EmailRequired") ?? "Please enter an email address.";
        public static string Validation_EmailInvalid => ResourceManager.GetString("Validation_EmailInvalid") ?? "Please enter a valid email address.";
        public static string Validation_PasswordRequired => ResourceManager.GetString("Validation_PasswordRequired") ?? "Please enter a password.";
        public static string Validation_PasswordMinLength => ResourceManager.GetString("Validation_PasswordMinLength") ?? "Password must be at least 6 characters long.";
        public static string Validation_ConfirmPasswordRequired => ResourceManager.GetString("Validation_ConfirmPasswordRequired") ?? "Please confirm your password.";
        public static string Validation_PasswordsDoNotMatch => ResourceManager.GetString("Validation_PasswordsDoNotMatch") ?? "Passwords do not match.";        

        

        // Main Window Strings
        public static string MainWindow_Title => ResourceManager.GetString("MainWindow_Title") ?? "Photo Manager";
        public static string MainWindow_BrowseButton => ResourceManager.GetString("MainWindow_BrowseButton") ?? "Browse Photo";
        public static string MainWindow_UploadButton => ResourceManager.GetString("MainWindow_UploadButton") ?? "Upload Photo";

        // DataGrid Headers
        public static string DataGrid_HeaderId => ResourceManager.GetString("DataGrid_HeaderId") ?? "ID";
        public static string DataGrid_HeaderFileName => ResourceManager.GetString("DataGrid_HeaderFileName") ?? "FileName";
        public static string DataGrid_HeaderSize => ResourceManager.GetString("DataGrid_HeaderSize") ?? "Size (KB)";
        public static string DataGrid_HeaderUploadDate => ResourceManager.GetString("DataGrid_HeaderUploadDate") ?? "Upload Date";
        public static string DataGrid_HeaderWidth => ResourceManager.GetString("DataGrid_HeaderWidth") ?? "Width";
        public static string DataGrid_HeaderHeight => ResourceManager.GetString("DataGrid_HeaderHeight") ?? "Height";
    }
} 