using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    class FileSystem : IDataAccess
    {

        public FileSystem()
        {

        }

        public void CreateRouteImg(BinaryReader reader, string filePath)
        {
            if (File.Exists(filePath))
            {
                return;
            }
            Byte[] bytearr = reader.ReadBytes(1 * 1024 * 1024 * 10);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(bytearr, 0, bytearr.Length);
                fs.Flush();
                fs.Close();
            }

        }

        public bool AddTour(TourItem tour)
        {
            throw new NotImplementedException();
        }

        public List<TourItem> GetItems()
        {
            throw new NotImplementedException();
        }

        public List<TourLog> GetLogs(string ItemName)
        {
            throw new NotImplementedException();
        }

        public List<TourItem> GetTourInformation(string ItemName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTour(TourItem currentItem)
        {
            bool state = false;
            //if ( state = File.Exists(currentItem.Route))
            //{
            //    File.Delete(currentItem.Route);
            //}
            return state;
        }
    }
}
