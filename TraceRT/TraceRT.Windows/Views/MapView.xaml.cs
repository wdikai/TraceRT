using Bing.Maps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TraceRT.Models.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TraceRT.Views
{
    public sealed partial class MapView : UserControl
    {
        public MapView()
        {
            this.InitializeComponent();
        }

        private void ScalePlus(object sender, RoutedEventArgs e)
        {
            this.Map.SetZoomLevel(Map.ZoomLevel + 1);
        }

        private void ScaleMinus(object sender, RoutedEventArgs e)
        {
            this.Map.SetZoomLevel(Map.ZoomLevel - 1);
        }
    }
}
