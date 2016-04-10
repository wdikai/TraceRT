namespace TraceRT.DataAccess.Providers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TraceRT.Models.Enums;
    using TraceRT.Models.Models;

    public class TraceProvider
    {
        public const string PathToServise = @"http://localhost:35631/ping";
        
        private static readonly Lazy<TraceProvider> instance = new Lazy<TraceProvider>(() => new TraceProvider(), true);

        private TraceProvider()
        {
        }

        public static TraceProvider Instance 
        {
            get
            {
                return instance.Value;
            }
        }

        public async Task TraceAsync(TraceModel model, TraceSettings settings, CancellationToken token) 
        {
            for (int ttl = 1; ttl < settings.MaxHops; ttl++)
            {
                var reply = await PingProvider.Instance.EchoRequestAsync(settings.Host, 32, ttl, settings.TimeOut, token);
                model.Nodes.Add(reply);
                var hostGeoInformation = await HostGeoInformationProvider.Instance.GetHostGeoInformation(reply.Address, token);
                if (hostGeoInformation.Status == "success")
                {
                    model.NodesGeoInformation.Add(hostGeoInformation);
                }

                await Task.Delay(settings.TimeOut);
                if (reply.Status == IPStatus.Success)
                {
                    break;                    
                }
            }
            model.IsReady = true;
        }
    }
}