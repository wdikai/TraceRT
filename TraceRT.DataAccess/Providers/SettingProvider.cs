namespace TraceRT.DataAccess.Providers
{
    using System;
    using Windows.Storage;

    public class SettingProvider
    {
        private static readonly Lazy<SettingProvider> instance = new Lazy<SettingProvider>(() => new SettingProvider(), true);

        private ApplicationDataContainer localSettings; 

        private SettingProvider()
        {
            this.localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        }

        public static SettingProvider Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public void CreateContainer(string name)
        {
            this.localSettings.CreateContainer(name, ApplicationDataCreateDisposition.Always);
        }

        internal object GetValue(string containerName, string valueName)
        {
            object result = null;
            if (this.localSettings.Containers.ContainsKey(containerName))
            {
                result = this.localSettings.Containers[containerName].Values[valueName];
            }

            return result;
        }

        internal void SetValue(string containerName, string valueName, object value)
        {
            if (this.localSettings.Containers.ContainsKey(containerName))
            {
                this.localSettings.Containers[containerName].Values[valueName] = value;
            }
        }
    }
}
