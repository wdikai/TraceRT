namespace TraceRT.Helpers.Converters
{
    using Bing.Maps;
    using System;
    using System.Collections.Generic;
    using TraceRT.Models.Models;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class LocationsToBingLocationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            LocationCollection result = new LocationCollection();
            List<HostGeoInformation> locations = value as List<HostGeoInformation>;
            if (value != null) 
            {
                foreach (HostGeoInformation location in locations)
                {
                    Location tempLocation = new Location(location.Latitude, location.Longitude);
                    result.Add(tempLocation);
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
