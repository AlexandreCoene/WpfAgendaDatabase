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

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Identite nouveauContact = new Identite
            {
                Nom = TB_Nom.Text, // Ajoute le nom du contact
                Prenom = TB_Prenom.Text,
                Numero = TB_Numero.Text,
                Email = TB_Email.Text,
                Sexe = TB_Sexe.Text,

                // Gestion conditionnelle de la date de naissance
                DateDeNaissance = Cal_DateDeNaissance.SelectedDate.HasValue ? DateOnly.FromDateTime(Cal_DateDeNaissance.SelectedDate.Value) : null,
                VilleDeNaissance = TB_VilleDeNaissance.Text,
            };

            if (CB_Famille.IsChecked == true) nouveauContact.Relation = "Famille";
            else if (CB_Ami.IsChecked == true) nouveauContact.Relation = "Ami";
            else if (CB_Travail.IsChecked == true) nouveauContact.Relation = "Travail";

            dAO_Contact.Ajouter(nouveauContact); // Ajoute le contact à la base de données

            // Afficher un message de confirmation
            MessageBox.Show("Le contact a été ajouté avec succès.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

            // Réinitialiser les champs de saisie pour une nouvelle entrée
            TB_Nom.Text = string.Empty;
            TB_Prenom.Text = string.Empty;
            TB_Numero.Text = string.Empty;
            TB_Email.Text = string.Empty;
            TB_Sexe.Text = string.Empty;
            Cal_DateDeNaissance.SelectedDate = null; // Pas nécessaire de vérifier si une date a été sélectionnée
            TB_VilleDeNaissance.Text = string.Empty;
            CB_Famille.IsChecked = false; //Combobox
            CB_Ami.IsChecked = false;
            CB_Travail.IsChecked = false;
        }
    }
}