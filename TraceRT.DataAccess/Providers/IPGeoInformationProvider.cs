namespace TraceRT.DataAccess.Providers
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TraceRT.Models.Models;

    public class HostGeoInformationProvider
    {
        public const string PathToGeoIpServise = @"http://ip-api.com/json/";

        private static readonly Lazy<HostGeoInformationProvider> instance = new Lazy<HostGeoInformationProvider>(() => new HostGeoInformationProvider(), true);

        private HostGeoInformationProvider()
        {
        }

        public static HostGeoInformationProvider Instance 
        {
            get
            {
                return instance.Value;
            }
        }


        public async Task<HostGeoInformation> GetHostGeoInformation(string url, CancellationToken token)
        {
            var loacteionQueryString = string.Format("{0}/{1}", PathToGeoIpServise, url);
            var loacteionJson = await Util.GetJson(loacteionQueryString, token);
            return JsonConvert.DeserializeObject<HostGeoInformation>(loacteionJson);
        }
    }
}
