namespace TraceRT.Helpers.Converters
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = Visibility.Collapsed;
            if (value != null && parameter != null && (value is Enum))
            {
                var stringValue = value.ToString().ToLowerInvariant();
                var enums = parameter.ToString().ToLowerInvariant();

                if (enums.Contains(stringValue))
                {
                    result = Visibility.Visible;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
