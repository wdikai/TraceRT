namespace TraceRT.DataAccess.Providers
{
    using System;
    using TraceRT.Models.Models;

    public class PingSettingsProvider
    {
        private const string PingSettingContainer = "PingSettings";

        private static readonly Lazy<PingSettingsProvider> instance = new Lazy<PingSettingsProvider>(() => new PingSettingsProvider(), true);

        private PingSettingsProvider()
        {
        }

        public static PingSettingsProvider Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public bool CanPingUntilStopped
        {
            get
            {
                var result = false;
                var temp = this.GetValue("CanPingUntilStopped");
                if (temp != null)
                {
                    result = (bool)temp;
                }

                return result;
            }

            set 
            {
                this.SetValue("CanPingUntilStopped", value); 
            }
        }

        public bool CanPingIpV4
        {
            get
            {
                var result = false;
                var temp = this.GetValue("CanPingIpV4");
                if (temp != null)
                {
                    result = (bool)temp;
                }

                return result;
            }

            set
            {
                this.SetValue("CanPingIpV4", value);
            }
        }

        public bool CanPingIpV6
        {
            get
            {
                var result = false;
                var temp = this.GetValue("CanPingIpV6");
                if (temp != null)
                {
                    result = (bool)temp;
                }

                return result;
            }

            set
            {
                this.SetValue("CanPingIpV6", value); 
            }
        }

        public bool CanPingHyperV
        {
            get
            {
                var result = false;
                var temp = this.GetValue("CanPingHyperV");
                if (temp != null)
                {
                    result = (bool)temp;
                }

                return result;
            }

            set
            {
                this.SetValue("CanPingHyperV", value);
            }
        }

        public int RequestNumber
        {
            get
            {
                var result = 4;
                var temp = this.GetValue("RequestNumber");
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

                this.SetValue("RequestNumber", value); 
            }
        }

        public int BufferLength
        {
            get
            {
                var result = 32;
                var temp = this.GetValue("BufferLength");
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

                this.SetValue("BufferLength", value); 
            }
        }

        public int Ttl
        {
            get
            {
                var result = 100;
                var temp = this.GetValue("Ttl");
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

                this.SetValue("Ttl", value); 
            }
        }

        public int TimeOut
        {
            get
            {
                var result = 100;
                var temp = this.GetValue("TimeOut");
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

        public void Load()
        {
            SettingProvider.Instance.CreateContainer(PingSettingContainer);
        }

        public PingSettings GetCurrentPingSettings()
        {
            var result = new PingSettings
            {
                CanPingUntilStopped = this.CanPingUntilStopped,
                CanPingIpV4 = this.CanPingIpV4,
                CanPingIpV6 = this.CanPingIpV6,
                CanPingHyperV = this.CanPingHyperV,
                RequestNumber = this.RequestNumber,
                BufferLength = this.BufferLength,
                Ttl = this.Ttl,
                TimeOut = this.TimeOut
            };

            return result;
        }

        private object GetValue(string valueName)
        {
            return SettingProvider.Instance.GetValue(PingSettingContainer, valueName);
        }

        private void SetValue(string valueName, object value)
        {
            SettingProvider.Instance.SetValue(PingSettingContainer, valueName, value);
        }
    }
}