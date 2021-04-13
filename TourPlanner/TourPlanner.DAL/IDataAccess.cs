using System;
using System.Collections.Generic;
using System.Text;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    interface IDataAccess
    {
        public List<TourItem> GetItems();

        public List<TourLog> GetLogs(string ItemName);

        public List<TourItem> GetTourInformation(string ItemName);

        public bool AddTour(TourItem tour);
        public bool DeleteTour(TourItem currentItem);
    }
}
