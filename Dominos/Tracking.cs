using System.IO;
using System.Net;

namespace Dominos_API
{
    public static class Tracking
    {
        public static DataEntities.Tracker TrackOrder(string pulseOrderGUID)
        {
            var url = $"https://tracker.dominos.com/tracker-presentation-service/v2/orders/stores/1554/orderkeys/155488126940/pulseorderguids/{pulseOrderGUID}";

            var html = GetTrackingRequest(url);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.Tracker>(html);
        }

        internal static string GetTrackingRequest(string url)
        {
            var html = string.Empty;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            request.Headers.Add("DPZ-Language:en");
            request.Headers.Add("DPZ-Market:UNITED_STATES");

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }
    }
}
