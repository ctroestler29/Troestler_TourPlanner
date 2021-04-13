using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class TourItemDAO
    {
        private IDataAccess dataAccess;

        public TourItemDAO()
        {
            //check which datasource to use (if, switch, ....)
            dataAccess = new Database();
            //dataAccess = new FileSystem();
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

        public bool AddTour(TourItem tour)
        {
            return dataAccess.AddTour(tour);
        }

        public bool DeleteTour(TourItem currentItem)
        {
            return dataAccess.DeleteTour(currentItem);
        }

    }
}
