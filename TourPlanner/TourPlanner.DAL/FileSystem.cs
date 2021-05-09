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

                Byte[] lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
                using (FileStream lxFS = new FileStream(filePath, FileMode.Create))
                {
                    lxFS.Write(lnByte, 0, lnByte.Length);
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
            throw new NotImplementedException();
        }
    }
}
