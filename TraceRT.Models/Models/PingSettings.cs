namespace TraceRT.Models.Models
{
    public class PingSettings
    {
        public string Host { get; set; }

        public bool CanPingUntilStopped { get; set; }

        public bool CanPingHyperV { get; set; }

        public bool CanPingIpV4 { get; set; }

        public bool CanPingIpV6 { get; set; }

        public int BufferLength { get; set; }

        public int RequestNumber { get; set; }

        public int Ttl { get; set; }

        public int TimeOut { get; set; }
    }
}
