using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PhotoManager.Wpf.Converters
{
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEmpty = string.IsNullOrWhiteSpace(value as string);
            
            // If parameter is "Invert", we want to show placeholder when field is empty
            if (parameter != null && parameter.ToString() == "Invert")
            {
                isEmpty = !isEmpty;
            }
            
            if (targetType == typeof(Visibility))
            {
                return isEmpty ? Visibility.Visible : Visibility.Collapsed;
            }
            
            return isEmpty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}