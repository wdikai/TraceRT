namespace TraceRT.Helpers.Converters
{
    using System;
    using System.Collections;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class EmptyToVisibilityConverter : IValueConverter
    {
        public bool IsReversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = Visibility.Collapsed;
            var isVisible = value != null;
            if (isVisible)
            {
                var collection = value as ICollection;
                if (collection != null)
                {
                    isVisible = collection.Count != 0;
                }
            }

            if (this.IsReversed)
            {
                isVisible = !isVisible;
            }

            if (isVisible)
            {
                result = Visibility.Visible;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
