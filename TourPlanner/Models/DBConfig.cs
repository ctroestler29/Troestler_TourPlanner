using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class DBConfig
    {
        //"Server=127.0.0.1;Port=5432;Database=TourPlannerDB;User Id=postgres;Password=root;";
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
