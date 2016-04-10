using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TraceRT.Models.Models
{
    public class HostGeoInformation
    {
        [JsonProperty("query")]
        public string Host { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
