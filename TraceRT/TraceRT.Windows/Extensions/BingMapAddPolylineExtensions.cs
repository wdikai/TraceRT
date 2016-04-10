namespace TraceRT.Helpers.Extensions
{
    using Bing.Maps;
    using Microsoft.Xaml.Interactivity;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TraceRT.Models.Models;
    using Windows.ApplicationModel;
    using Windows.UI;
    using Windows.UI.Xaml;

    public class BingMapPolylineExtension : DependencyObject
    {
        public static readonly DependencyProperty LocationsProperty =
           DependencyProperty.RegisterAttached("Locations", typeof(LocationCollection),
           typeof(BingMapPolylineExtension), new PropertyMetadata(default(LocationCollection), LocationsPropertyChanged));

        public static void SetLocations(DependencyObject d, object value)
        {
            d.SetValue(LocationsProperty, value);
        }

        public static LocationCollection GetLocations(DependencyObject d)
        {
            return (LocationCollection)d.GetValue(LocationsProperty);
        }

        private static void LocationsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine("old {0}, new {1}", e.OldValue, e.NewValue);
            if (e.OldValue != e.NewValue)
            {
                var line = d as MapPolyline;
                if (line != null)
                {
                    line.Locations = e.NewValue as LocationCollection;
                }
            }
        }
    }
}