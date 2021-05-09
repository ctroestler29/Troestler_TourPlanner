using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ITourItemFactory
    {
        IEnumerable<TourItem> GetItems();
        IEnumerable<TourLog> GetLogs(string ItemName);
        IEnumerable<TourItem> GetTourInformation(string ItemName);
        IEnumerable<TourItem> Search(string itemName, bool caseSensitive = false);
        Task<bool> AddTourAsync(TourRequest tour);
        bool DeleteTour(TourItem currentItem);
    }
}
