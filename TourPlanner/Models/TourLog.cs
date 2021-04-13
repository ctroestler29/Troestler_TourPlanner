using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourLog
    {
        public string TourName { get; set; }
        public DateTime Date { get; set; }
        public DateTime Duration { get; set; }
        public int Distance { get; set; }
    }
}
