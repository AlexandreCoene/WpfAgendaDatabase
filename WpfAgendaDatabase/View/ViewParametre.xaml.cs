using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAgendaDatabase.View
{
    /// <summary>
    /// Logique d'interaction pour ViewParametre.xaml
    /// </summary>
    public partial class ViewParametre : UserControl
    {
        public ViewParametre()
        {
            InitializeComponent();
            TB_Nom_BDD.Text = "agenda_alex";
            TB_IP_BDD.Text = "localhost";
            TB_Nom_Utilisateur.Text = "root";
            TB_MDP_BDD.Text = "";
        }

        private void Button_Click_Valider_Parametre(object sender, RoutedEventArgs e)
        {
            string ServerName = TB_IP_BDD.Text;
            string Username = TB_Nom_Utilisateur.Text;
            string Password = TB_MDP_BDD.Text;
            string Database = TB_Nom_BDD.Text;



            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["host"].Value = ServerName;
            config.AppSettings.Settings["user"].Value = Username;
            config.AppSettings.Settings["password"].Value = Password;
            config.AppSettings.Settings["database"].Value = Database;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            MainContent.Content = new ViewAccueil(); 
        }
    }
}
