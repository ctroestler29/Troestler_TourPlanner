using System;
using System.Collections.Generic;
using System.Linq;
using TourPlanner.Models;
using TourPlanner.DAL;
using System.Threading.Tasks;

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

        public async Task<bool> AddTourAsync(TourRequest tour)
        {
            return await tourItemDAO.AddTourAsync(tour);
        }

        public bool DeleteTour(TourItem currentItem)
        {
            return tourItemDAO.DeleteTour(currentItem);
        }

    }
}
