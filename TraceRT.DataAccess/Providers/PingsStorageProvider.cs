namespace TraceRT.DataAccess.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TraceRT.Models.Models;

    public class PingStorageProvider
    {
        private const string Path = "Pings.json";

        private static readonly Lazy<PingStorageProvider> instance = new Lazy<PingStorageProvider>(() => new PingStorageProvider(), true);

        private PingStorageProvider()
        {
            this.Pings = new List<PingModel>();
        }

        public static PingStorageProvider Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public List<PingModel> Pings { get; private set; }

        public async Task AddAsync(PingModel ping)
        {
            this.Pings.Insert(0, ping);
            await this.SaveAsync();
        }

        public async Task RemoveAsync(PingModel ping)
        {
            this.Pings.Remove(ping);
            await this.SaveAsync();
        }

        public async Task ClearAsync()
        {
            this.Pings.Clear();
            await this.SaveAsync();
        }

        public async Task SaveAsync()
        {
            await StorageProvider.Instance.SaveAsync<List<PingModel>>(this.Pings, Path);
        }

        public async Task LoadAsync()
        {
            var tempPings = await StorageProvider.Instance.LoadAsync<List<PingModel>>(Path);
            if (tempPings != null)
            {
                this.Pings.AddRange(tempPings);
            }
        }
    }
}
