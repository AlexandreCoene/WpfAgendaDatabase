using System;
using System.Collections.Generic;
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
using WpfAgendaDatabase.Agenda_AlexDB;
using WpfAgendaDatabase.Service.DAO;

namespace WpfAgendaDatabase.View
{
    public partial class ViewAjouter : UserControl
    {
        private DAO_Contact dAO_Contact;

        public ViewAjouter()
        {
            InitializeComponent();
            dAO_Contact = new DAO_Contact(); // Initialisation de DAO_Contact
        }

        // Dans le fichier de votre UserControl ou de votre fenêtre où vous gérez les événements
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            var nouvelIdentite = new Identité
            {
                Nom = TB_Nom.Text,
                Prenom = TB_Prenom.Text,
                Numero = TB_Numero.Text,
                Email = TB_Email.Text,
                // Assurez-vous de définir correctement toutes les autres propriétés nécessaires
            };

            // Ajouter le nouvel identité à la base de données via DAO_Contact
            dAO_Contact.Ajouter(nouvelIdentite);

            // Afficher un message de confirmation
            MessageBox.Show("Contact ajouté avec succès!");

            // Optionnel: Réinitialiser les champs du formulaire après l'ajout
            TB_Nom.Clear();
            TB_Prenom.Clear();
            TB_Numero.Clear();
            TB_Email.Clear();
            // Réinitialiser ici les autres champs TextBox si nécessaire
        }


    }
}