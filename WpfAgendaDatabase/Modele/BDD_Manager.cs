using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAgendaDatabase.Modele
{

    public class BDD_Manager
    {
        public string host { get; set; }
        public string port { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string database { get; set; }
        public string mysqlVer { get; set; }

        public string connectionString
        {
            get
            {
                return $"server={host};port={port};user={user};password={password};database={database}";
            }
        }
        public BDD_Manager()
        {
            host = ConfigurationManager.AppSettings["host"];
            port = ConfigurationManager.AppSettings["port"];
            user = ConfigurationManager.AppSettings["user"];
            password = ConfigurationManager.AppSettings["password"];
            database = ConfigurationManager.AppSettings["database"];
            mysqlVer = ConfigurationManager.AppSettings["mysqlVer"];
        }

        public void Save_BDD_Settings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["host"].Value = host;
            config.AppSettings.Settings["port"].Value = port;
            config.AppSettings.Settings["user"].Value = user;
            config.AppSettings.Settings["password"].Value = password;
            config.AppSettings.Settings["database"].Value = database;
            config.AppSettings.Settings["mysqlVer"].Value = mysqlVer;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
