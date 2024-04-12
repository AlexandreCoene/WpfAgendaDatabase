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
using WpfAgendaDatabase.Modele;

namespace WpfAgendaDatabase.View
{
    /// <summary>
    /// Logique d'interaction pour ViewParametre.xaml
    /// </summary>
    public partial class ViewParametre : UserControl
    {
        BDD_Manager bDD_Manager;
        public ViewParametre()
        {
            InitializeComponent();
            bDD_Manager = new BDD_Manager();

        }

        private void Button_Click_Valider_Parametre(object sender, RoutedEventArgs e)
        {

            bDD_Manager.host = TB_IP_BDD.Text;
            bDD_Manager.user = TB_Nom_Utilisateur.Text;
            bDD_Manager.password = TB_MDP_BDD.Text;
            bDD_Manager.database = TB_Nom_BDD.Text;

            bDD_Manager.Save_BDD_Settings();

            MessageBox.Show("Paramètres enregistrés");
        }
    }
}
