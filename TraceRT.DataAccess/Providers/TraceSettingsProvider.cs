namespace TraceRT.DataAccess.Providers
{
    using System;
    using TraceRT.Models.Models;

    public class TraceSettingsProvider
    {
        private const string TraceSettingContainer = "TraceSettings";
        private static readonly Lazy<TraceSettingsProvider> instance = new Lazy<TraceSettingsProvider>(() => new TraceSettingsProvider(), true);

        private TraceSettingsProvider()
        {
        }

        public static TraceSettingsProvider Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public bool IsTraceDontResolveAddress
        {
            get
            {
                bool result = true;
                object temp = this.GetValue("IsTraceDontResolveAddress");
                if (temp != null) 
                {
                    result = (bool)temp;
                }

                return result;
            }

            set
            {
                this.SetValue("IsTraceDontResolveAddress", value);
            }
        }

        public bool RoundTrip
        {
            get
            {
                bool result = false;
                object temp = this.GetValue("RoundTrip");
                if (temp != null)
                {
                    result = (bool)temp;
                }

                return result;
            }

            set
            {
                this.SetValue("RoundTrip", value);
            }
        }

        public bool CanTraceIpV4
        {
            get
            {
                bool result = false;
                object temp = this.GetValue("CanTrace");
                if (temp != null)
                {
                    result = (bool)temp;
                }

                return result;
            }

            set
            {
                this.SetValue("CanTrace", value);
            }
        }

        public bool CanTraceIpV6
        {
            get
            {
                bool result = false;
                object temp = this.GetValue("CanTraceIpV6");
                if (temp != null)
                {
                    result = (bool)temp;
                }

                return result;
            }

            set
            {
                this.SetValue("CanTraceIpV6", value);
            }
        }

        public int MaxHops
        {
            get
            {
                int result = 100;
                object temp = this.GetValue("MaxHops");
                if (temp != null)
                {
                    result = (int)temp;
                }

                return result;
            }

            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                this.SetValue("MaxHops", value);
            }
        }

        public int LooseSource
        {
            get
            {
                int result = 4;
                object temp = this.GetValue("LooseSource");
                if (temp != null)
                {
                    result = (int)temp;
                }

                return result;
            }

            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                this.SetValue("LooseSource", value);
            }
        }

        public int TimeOut
        {
            get
            {
                int result = 100;
                object temp = this.GetValue("TimeOut");
                if (temp != null)
                {
                    result = (int)temp;
                }

                return result;
            }

            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                this.SetValue("TimeOut", value);
            }
        }

        public string Source
        {
            get
            {
                string result = string.Empty;
                object temp = this.GetValue("Source");
                if (temp != null)
                {
                    result = (string)temp;
                }

                return result;
            }

            set
            {
                this.SetValue("Source", value);
            }
        }

        public void Load()
        {
            SettingProvider.Instance.CreateContainer(TraceSettingContainer);
        }

        public TraceSettings CreateSettings()
        {
            var result = new TraceSettings
            {
                IsTraceDontResolveAddress = this.IsTraceDontResolveAddress,
                RoundTrip = this.RoundTrip,
                CanTraceIpV4 = this.CanTraceIpV4,
                CanTraceIpV6 = this.CanTraceIpV6,
                MaxHops = this.MaxHops,
                TimeOut = this.TimeOut,
                Source = this.Source
            };

            return result;
        }

        private object GetValue(string valueName)
        {
            return SettingProvider.Instance.GetValue(TraceSettingContainer, valueName);
        }

        private void SetValue(string valueName, object value)
        {
            SettingProvider.Instance.SetValue(TraceSettingContainer, valueName, value);
        }
    }
}
