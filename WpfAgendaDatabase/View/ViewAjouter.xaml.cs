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
            // Créez une nouvelle instance de Identité avec les informations collectées.
            Identité nouveauContact = new Identité
            {
                Nom = TB_Nom.Text,
                Prenom = TB_Prenom.Text,
                Numero = TB_Numero.Text,
                Email = TB_Email.Text,
                Sexe = TB_Sexe.Text,
                DateDeNaissance = DateOnly.Parse(TB_Datedenaissance.Text), // Assurez-vous que le format de la date est correct
                VilleDeNaissance = TB_VilleDeNaissance.Text,
                // Les autres propriétés peuvent être initialisées ici si nécessaire
            };

            // Ajoutez la relation en fonction des cases à cocher
            if (CB_Famille.IsChecked == true)
            {
                nouveauContact.Relation = "Famille";
            }
            else if (CB_Ami.IsChecked == true)
            {
                nouveauContact.Relation = "Ami";
            }
            else if (CB_Travail.IsChecked == true)
            {
                nouveauContact.Relation = "Travail";
            }

            // Utilisez DAO_Contact pour ajouter le nouveau contact et sa relation à la base de données
            var dAO_Contact = new DAO_Contact();
            dAO_Contact.Ajouter(nouveauContact);
        }

    }
}