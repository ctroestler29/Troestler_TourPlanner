using System;
using System.Collections.Generic;
using System.Linq;
using TourPlanner.Models;
using TourPlanner.DAL;

namespace TourPlanner.BusinessLayer
{
    internal class TourItemFactoryImpl : ITourItemFactory
    {
        private TourItemDAO tourItemDAO = new TourItemDAO();

        public IEnumerable<TourItem> GetItems()
        {
            return tourItemDAO.GetItems();
        }

        public IEnumerable<TourLog> GetLogs(string ItemName )
        {
            return tourItemDAO.GetLogs(ItemName);
        }

        public IEnumerable<TourItem> GetTourInformation(string ItemName)
        {
            return tourItemDAO.GetTourInformation(ItemName);
        }

        public IEnumerable<TourItem> Search(string itemName, bool caseSensitive = false)
        {
            IEnumerable<TourItem> items = GetItems();

            if(caseSensitive)
            {
                return items.Where(x => x.Name.Contains(itemName));
            }
            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }

        public bool AddTour(TourItem tour)
        {
            return tourItemDAO.AddTour(tour);
        }

        public bool DeleteTour(TourItem currentItem)
        {
            return tourItemDAO.DeleteTour(currentItem);
        }

    }
}
