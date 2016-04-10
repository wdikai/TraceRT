namespace TraceRT.Helpers.TemplateSelectors
{
    using TraceRT.Models.Enums;
    using TraceRT.Models.Models;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class TraceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SuccessTemplate { get; set; }

        public DataTemplate WarningTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var result = base.SelectTemplateCore(item, container);
            var tempItem = item as EchoReply;
            if (tempItem != null)
            {
                switch (tempItem.Status)
                {
                    case IPStatus.Success:
                    case IPStatus.TtlExpired:
                        result = this.SuccessTemplate;
                        break;
                    case IPStatus.TimedOut:
                        result = this.WarningTemplate;
                        break;
                }
            }

            return result;
        }
    }
}
