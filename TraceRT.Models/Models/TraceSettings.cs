namespace TraceRT.Models.Models
{
    public class TraceSettings
    {
        public string Host { get; set; }

        public bool IsTraceDontResolveAddress { get; set; }

        public bool RoundTrip { get; set; }

        public bool CanTraceIpV4 { get; set; }

        public bool CanTraceIpV6 { get; set; }

        public string Source { get; set; }

        public int MaxHops { get; set; }

        public int TimeOut { get; set; }
    }
}
