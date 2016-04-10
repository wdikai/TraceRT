namespace TraceRT.Helpers.TemplateSelectors
{
    using TraceRT.Models.Enums;
    using TraceRT.Models.Models;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class PingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ErrorTemplate { get; set; }

        public DataTemplate TimeOutTemplate { get; set; }

        public DataTemplate SuccessTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var result = this.ErrorTemplate;

            var tempItem = item as EchoReply;
            if (tempItem != null)
            {
                switch (tempItem.Status)
                {
                    case IPStatus.Success:
                        result = this.SuccessTemplate;
                        break;
                    case IPStatus.TimedOut:
                        result = this.TimeOutTemplate;
                        break;
                }
            }

            return result;
        }
    }
}
