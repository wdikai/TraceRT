namespace TraceRT.DataAccess.Providers
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using TraceRT.Models.Enums;
    using TraceRT.Models.Models;

    public class PingProvider
    {
        public const string PathToServise = @"http://localhost:35631/ping";

        private static readonly Lazy<PingProvider> instance = new Lazy<PingProvider>(() => new PingProvider(), true);

        private PingProvider()
        {
        }

        public static PingProvider Instance 
        {
            get
            {
                return instance.Value;
            }
        }

        public async Task PingAsync(PingModel ping, PingSettings setting, CancellationToken token) 
        {            
            ping.Start();
            var workTimer = Stopwatch.StartNew();
            if (setting.CanPingUntilStopped) 
            {
                await this.UntilStoppedPingAsync(ping, setting, token);
            }
            else
            {
                await this.CountPingAsync(ping, setting, token);
            }

            workTimer.Stop();
            ping.TimeStamp = workTimer.ElapsedMilliseconds;
            ping.Finish();
        }

        public async Task<EchoReply> EchoRequestAsync(string host, int bufferSize, int ttl, int timeout, CancellationToken token)
        {
            var result = new EchoReply();
            var queryString = string.Format("{0}/{1}/{2}/{3}/{4}/", PathToServise, host, bufferSize, ttl, timeout);
            var json = await Util.GetJson(queryString, token);
            var tempResult = JsonConvert.DeserializeObject<EchoReply>(json);
            if (tempResult != null)
            {
                result = tempResult;
            }
            else
            {
                result.Status = IPStatus.Invalide;
            }

            return result;
        }

        private async Task CountPingAsync(PingModel ping, PingSettings setting, CancellationToken token)
        {
            for (int i = 0; i < setting.RequestNumber && ping.IsWorking; i++)
            {
                try
                {
                    var newPing = await this.EchoRequestAsync(setting.Host, setting.BufferLength, setting.Ttl, setting.TimeOut, token);
                    ping.Add(newPing);
                    await Task.Delay(500);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private async Task UntilStoppedPingAsync(PingModel pingModel, PingSettings setting, CancellationToken token)
        {
            while (pingModel.IsWorking)
            {
                try
                {
                    var newPing = await this.EchoRequestAsync(setting.Host, setting.BufferLength, setting.Ttl, setting.TimeOut, token);
                    pingModel.Add(newPing);
                    await Task.Delay(500);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}