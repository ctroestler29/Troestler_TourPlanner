using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace TourPlanner.DAL
{
    public class TourItemDAO
    {
        private IDataAccess dataAccess;
        private IDataAccess dataAccess2;
        private static readonly HttpClient client = new HttpClient();

        public TourItemDAO()
        {
            //check which datasource to use (if, switch, ....)
            dataAccess = new Database();
            dataAccess2 = new FileSystem();
        }

        public List<TourItem> GetItems()
        {
            return dataAccess.GetItems();
        }

        public List<TourLog> GetLogs(string ItemName)
        {
            return dataAccess.GetLogs(ItemName);
        }

        public List<TourItem> GetTourInformation(string ItemName)
        {
            return dataAccess.GetTourInformation(ItemName);
        }

        public async Task<bool> AddTourAsync(TourRequest tour)
        {
            string url = "http://www.mapquestapi.com/directions/v2/route?key=E7OXb2VjmZlm3d3HeUbnmw4dDXp2Qi3n&from=" + tour.Start + "&to=" + tour.Destination + "&outFormat=json";
            var response = await client.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();
            JObject o = JObject.Parse(responseString);
            Console.WriteLine(o.SelectToken("sessionId"));
            string lrlat = o.SelectToken("route").SelectToken("boundingBox").SelectToken("lr").SelectToken("lat").ToString().Replace(",",".");
            string lrlng = o.SelectToken("route").SelectToken("boundingBox").SelectToken("lr").SelectToken("lng").ToString().Replace(",", ".");
            string ullat = o.SelectToken("route").SelectToken("boundingBox").SelectToken("ul").SelectToken("lat").ToString().Replace(",", ".");
            string ullng = o.SelectToken("route").SelectToken("boundingBox").SelectToken("ul").SelectToken("lng").ToString().Replace(",", ".");
            string url2 = "https://www.mapquestapi.com/staticmap/v5/map?key=E7OXb2VjmZlm3d3HeUbnmw4dDXp2Qi3n&size=640,480@2x&defaultMarker=none&zoom=11&rand=737758036&session=" + o.SelectToken("route").SelectToken("sessionId")+"&boundingBox="+lrlat+","+ lrlng + "," + ullat + "," + ullng;
            var response2 = await client.GetAsync(url2);

            //var responseString2 = await response2.Content.ReadAsStringAsync();
            //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(responseString2);
            //var test = Convert.ToBase64String(plainTextBytes);
            //var bytes = Convert.FromBase64String(test);

            //using (var imageFile = new FileStream(filePath, FileMode.Create))
            //{
            //    imageFile.Write(bytes, 0, bytes.Length);
            //    imageFile.Flush();
            //}
            string filePath = "D:\\FH2021\\SWE\\Troestler_TourPlanner\\TourPlanner\\TourPlanner\\bin\\images\\"+tour.Start+"_"+tour.Destination+".jpg";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url2);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse lxResponse = (HttpWebResponse)request.GetResponse())
            {
                using (BinaryReader reader = new BinaryReader(lxResponse.GetResponseStream()))
                {
                    dataAccess2.CreateRouteImg(reader, filePath);
                }
            }


            TourItem Ntour = new()
            {
               Name = tour.Name,
               Route = filePath,
               Description =""

            };

            return dataAccess.AddTour(Ntour);
        }

        public bool DeleteTour(TourItem currentItem)
        {
            
            return dataAccess.DeleteTour(currentItem);
        }

    }
}
