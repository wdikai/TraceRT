namespace TraceRT.DataAccess.Providers
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Util
    {
        internal static async Task<string> GetJson(string url, CancellationToken token)
        {
            var jsonResponse = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(url, token))
                    {
                        jsonResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (HttpRequestException)
            {

            }
            catch(TaskCanceledException)
            {

            }

            return jsonResponse;
        }
    }
}
