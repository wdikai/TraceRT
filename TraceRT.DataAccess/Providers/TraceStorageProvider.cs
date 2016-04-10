using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceRT.Models.Models;

namespace TraceRT.DataAccess.Providers
{
    public class TraceStorageProvider
    {
        private const string Path = "Traces.json";

        private static readonly Lazy<TraceStorageProvider> instance = new Lazy<TraceStorageProvider>(() => new TraceStorageProvider(), true);

        private TraceStorageProvider()
        {
            this.Traces = new List<TraceModel>();
        }

        public static TraceStorageProvider Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public List<TraceModel> Traces { get; private set; }

        public async Task AddAsync(TraceModel trace)
        {
            this.Traces.Insert(0, trace);
            await this.SaveAsync();
        }

        public async Task RemoveAsync(TraceModel trace)
        {
            this.Traces.Remove(trace);
            await this.SaveAsync();
        }

        public async Task ClearAsync()
        {
            this.Traces.Clear();
            await this.SaveAsync();
        }

        public async Task SaveAsync()
        {
            await StorageProvider.Instance.SaveAsync<List<TraceModel>>(this.Traces, Path);
        }

        public async Task LoadAsync()
        {
            List<TraceModel> tempTraces = await StorageProvider.Instance.LoadAsync<List<TraceModel>>(Path);
            if (tempTraces != null)
            {
                this.Traces.AddRange(tempTraces);
            }
        }
    }
}
