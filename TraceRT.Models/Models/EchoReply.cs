namespace TraceRT.Models.Models
{
    using TraceRT.Models.Enums;

    public class EchoReply
    {
        public string Address { get; set; }

        public long Ttl { get; set; }

        public IPStatus Status { get; set; }

        public long RoundtripTime { get; set; }

        public byte[] Buffer { get; set; }
    }
}
