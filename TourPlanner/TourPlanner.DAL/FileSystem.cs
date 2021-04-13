using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    class FileSystem : IDataAccess
    {
        public List<TourItem> tours = new List<TourItem>()
            {
                new TourItem(){Name="Tour1", Route="Route information1", Description="Very Hard"},
                new TourItem(){Name="Tour2", Route="Route information2", Description="Very Easy"}
            };

        private string filePath;
        public FileSystem()
        {
            filePath = "";
        }

        public bool AddTour(TourItem tour)
        {
            foreach (var item in tours)
            {
                if (item.Name == tour.Name)
                {
                    return false;
                }
            }

            tours.Add(tour);
            return true;
        }

        public List<TourItem> GetItems()
        {
            //get tour items from file system
            return tours;
        }

        public List<TourLog> GetLogs(string ItemName)
        {
            //Select sql query
            List<TourLog> logs = new List<TourLog>()
            {
                new TourLog(){TourName="Tour1", Date=DateTime.Now, Duration= DateTime.Now, Distance=80 },
                new TourLog(){TourName="Tour2", Date=DateTime.Now, Duration=DateTime.Now, Distance=130},
                new TourLog(){TourName="Tour2", Date=DateTime.Now, Duration=DateTime.Now, Distance=350}
            };

            List<TourLog> logsfiltered = new List<TourLog>();
            foreach (var item in logs)
            {
                if (item.TourName == ItemName)
                {
                    logsfiltered.Add(item);
                }
            }

            return logsfiltered;
        }

        public List<TourItem> GetTourInformation(string ItemName)
        {
            //Select sql query
            
            List<TourItem> logsfiltered = new List<TourItem>();
            foreach (var item in tours)
            {
                if (item.Name == ItemName)
                {
                    logsfiltered.Add(item);
                }
            }

            return logsfiltered;
        }

        public bool DeleteTour(TourItem currentItem)
        {
            bool state=false;
            if(tours.Contains(currentItem))
            {
                tours.Remove(currentItem);
                state = true;
            }
            return state;
        }
    }
}
