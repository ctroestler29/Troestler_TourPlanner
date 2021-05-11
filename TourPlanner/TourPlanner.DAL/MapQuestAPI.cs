using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class MapQuestAPI
    {
        private ConfigFile configfile;
        private static readonly HttpClient client = new HttpClient();
        public MapQuestAPI()
        {
            string path = Directory.GetCurrentDirectory();
            string jsontxt = File.ReadAllText(path + "\\configfile.json");
            configfile = JsonConvert.DeserializeObject<ConfigFile>(jsontxt);
        }

        public async Task<string> GetRouteAsync(TourRequest tour)
        {
            string url = "http://www.mapquestapi.com/directions/v2/route?key=" + configfile.mapquestconfig.APIKey + "&from=" + tour.Start + "&to=" + tour.Destination + "&outFormat=json";
            var response = await client.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();
            JObject o = JObject.Parse(responseString);
            Console.WriteLine(o.SelectToken("sessionId"));
            string lrlat = o.SelectToken("route").SelectToken("boundingBox").SelectToken("lr").SelectToken("lat").ToString().Replace(",", ".");
            string lrlng = o.SelectToken("route").SelectToken("boundingBox").SelectToken("lr").SelectToken("lng").ToString().Replace(",", ".");
            string ullat = o.SelectToken("route").SelectToken("boundingBox").SelectToken("ul").SelectToken("lat").ToString().Replace(",", ".");
            string ullng = o.SelectToken("route").SelectToken("boundingBox").SelectToken("ul").SelectToken("lng").ToString().Replace(",", ".");
            string url2 = "https://www.mapquestapi.com/staticmap/v5/map?key=" + configfile.mapquestconfig.APIKey + "&size=640,480@2x&defaultMarker=none&zoom=11&rand=737758036&session=" + o.SelectToken("route").SelectToken("sessionId") + "&boundingBox=" + lrlat + "," + lrlng + "," + ullat + "," + ullng;
            
            return url2;
        }

        public BinaryReader GetImage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse Response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new BinaryReader(Response.GetResponseStream()))
                {
                    return reader;
                }
            }
        }
    }
}
