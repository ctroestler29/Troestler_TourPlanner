using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class ConfigFile
    {
        public DBConfig dbConfig { get; set; }
        public FileSystemConfig filesystemconfig { get; set; }
        public MapQuestConfig mapquestconfig { get; set; }
    }
}
