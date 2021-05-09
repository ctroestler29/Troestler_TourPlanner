using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using Npgsql;
using System.IO;
using Newtonsoft.Json;

namespace TourPlanner.DAL
{
    class Database : IDataAccess
    {
       
        private ConfigFile configfile;
        private string connectionString;
        NpgsqlConnection connection;
        public Database()
        {
            string path = Directory.GetCurrentDirectory();
            string jsontxt = File.ReadAllText(path + "\\configfile.json");
            configfile = JsonConvert.DeserializeObject<ConfigFile>(jsontxt);
            //get connectionString from config file
            connectionString = "Server="+configfile.dbConfig.Server+ ";Port=" + configfile.dbConfig.Port + ";Database=" + configfile.dbConfig.Database + ";User Id=" + configfile.dbConfig.UserId + ";Password=" + configfile.dbConfig.Password + ";";
            connection = new NpgsqlConnection(connectionString);
        }
        public List<TourItem> GetItems()
        {
            //Select sql query
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM touritem;", connection);

            //Act
            NpgsqlDataReader dataReader = command.ExecuteReader();
            List<TourItem> tourss = new List<TourItem>();
            for (int i = 0; dataReader.Read(); i++)
            {
                TourItem titem = new TourItem() { Name = dataReader[0].ToString(), Route = dataReader[1].ToString(), Description = dataReader[2].ToString() };
                tourss.Add(titem);
            }
            connection.Close();
            return tourss;
        }

        public List<TourLog> GetLogs(string ItemName)
        {
            //Select sql query
            //List<TourLog> logs = new List<TourLog>()
            //{
            //    new TourLog(){TourName="Tour1", Date=DateTime.Now, Duration= DateTime.Now, Distance=80 },
            //    new TourLog(){TourName="Tour2", Date=DateTime.Now, Duration=DateTime.Now, Distance=130},
            //    new TourLog(){TourName="Tour2", Date=DateTime.Now, Duration=DateTime.Now, Distance=350}
            //};

            //List<TourLog> logsfiltered = new List<TourLog>();
            //foreach (var item in logs)
            //{
            //    if(item.TourName==ItemName)
            //    {
            //        logsfiltered.Add(item);
            //    }
            //}

            //return logsfiltered;
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM tourlog WHERE tourname='"+ItemName+"';", connection);

            //Act
            NpgsqlDataReader dataReader = command.ExecuteReader();
            List<TourLog> tourslogs = new List<TourLog>();
            for (int i = 0; dataReader.Read(); i++)
            {
                TourLog titem = new TourLog() { TourName = dataReader[0].ToString(), Date = DateTime.Parse(dataReader[1].ToString()), Duration = DateTime.Parse(dataReader[2].ToString()), Distance = int.Parse(dataReader[3].ToString()) };
                tourslogs.Add(titem);
            }
            connection.Close();
            return tourslogs;
        }

        public List<TourItem> GetTourInformation(string ItemName)
        {
            //Select sql query
            //List<TourItem> logsfiltered = new List<TourItem>();
            //foreach (var item in tours)
            //{
            //    if (item.Name == ItemName)
            //    {
            //        logsfiltered.Add(item);
            //    }
            //}

            //return logsfiltered;

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM touritem WHERE tourname ='"+ItemName+"';", connection);

            //Act
            NpgsqlDataReader dataReader = command.ExecuteReader();
            List<TourItem> tourss = new List<TourItem>();
            for (int i = 0; dataReader.Read(); i++)
            {
                TourItem titem = new TourItem() { Name = dataReader[0].ToString(), Route = dataReader[1].ToString(), Description = dataReader[2].ToString() };
                tourss.Add(titem);
            }
            connection.Close();
            return tourss;
        }

        public bool AddTour(TourItem tour)
        {
            //foreach (var item in tours)
            //{
            //    if(item.Name==tour.Name)
            //    {
            //        return false;
            //    }
            //}

            //tours.Add(tour);
            //return true;

            connection.Open();
            NpgsqlCommand cmd2 = new NpgsqlCommand("insert into touritem (tourname, routeimgpath, description) values('" + tour.Name + "', '" + tour.Route + "', '" + tour.Description + "');", connection);
            cmd2.ExecuteNonQuery();
            connection.Close();

            return true;
        }

        public bool DeleteTour(TourItem currentItem)
        {
            //bool state = false;
            //if (tours.Contains(currentItem))
            //{
            //    tours.Remove(currentItem);
            //    state = true;
            //}
            //return state;

            connection.Open();
            NpgsqlCommand cmd5 = new NpgsqlCommand("DELETE FROM touritem WHERE tourname = '" + currentItem.Name + "';", connection);
            cmd5.ExecuteNonQuery();
            connection.Close();

            return true;
        }

        public void CreateRouteImg(BinaryReader reader, string name)
        {
            throw new NotImplementedException();
        }
    }
}
