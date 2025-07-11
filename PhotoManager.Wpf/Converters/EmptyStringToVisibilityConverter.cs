using System.Windows;
using System.Windows.Data;
using System.Globalization;


namespace PhotoManager.Wpf.Converters
{
    public class EmptyStringToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is not string valueStr) || targetType != typeof(Visibility))
                return !Invert ? Visibility.Collapsed : Visibility.Visible;

            bool isEmpty = string.IsNullOrWhiteSpace(valueStr);
            if (!Invert)
                return isEmpty ? Visibility.Collapsed : Visibility.Visible;
            else
                return isEmpty ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}