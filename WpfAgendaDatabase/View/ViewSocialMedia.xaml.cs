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
    /// <summary>
    /// Logique d'interaction pour ViewSocialMedia.xaml
    /// </summary>
    public partial class ViewSocialMedia : UserControl
    {
        public Identite SelectedContact { get; set; }

        public ViewSocialMedia()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        public void SetSelectedContact(Identite contact)
        {
            SelectedContact = contact;
            LoadSocialMediaProfiles();
        }

        private void LoadSocialMediaProfiles()
        {
            // Supposons que vous avez une méthode dans DAO_Contact pour obtenir les profils sociaux
            var socialProfiles = new DAO_Contact().GetSocialProfiles(SelectedContact.Idtable1);
            ListViewSocialMedia.ItemsSource = socialProfiles;
        }

        private void ButtonAddSocialMedia_Click(object sender, RoutedEventArgs e)
        {
            string socialMediaName = TextBoxSocialMediaName.Text;
            string socialMediaUrl = TextBoxSocialMediaUrl.Text;

            if (!string.IsNullOrWhiteSpace(socialMediaName) && SelectedContact != null)
            {
                // Créez un nouveau SocialMedium et SocialProfil
                SocialMedium newSocialMedia = new SocialMedium { Name = socialMediaName, Url = socialMediaUrl };
                SocialProfil newProfile = new SocialProfil
                {
                    // L'ID de SocialMedia sera rempli après la sauvegarde
                    IdentitéIdtable1 = SelectedContact.Idtable1
                    // Plus de propriétés si nécessaire
                };

                // Ajoutez le nouveau SocialMedium et sauvegardez pour générer l'ID
                new DAO_Contact().AddSocialProfil(newProfile, newSocialMedia);

                // Rafraîchir l'interface utilisateur, etc.
                LoadSocialMediaProfiles();
            }
        }

        private void ButtonDeleteSocialMedia_Click(object sender, RoutedEventArgs e)
        {
            var selectedSocialProfile = ListViewSocialMedia.SelectedItem as SocialProfil;
            if (selectedSocialProfile != null)
            {
                // DAO pour accéder à la base de données
                var daoContact = new DAO_Contact();
                daoContact.DeleteSocialProfile(selectedSocialProfile);

                // Mettre à jour l'affichage en enlevant le profil de la ListView
                LoadSocialMediaProfiles(); // Rechargez les profils pour mettre à jour la liste
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un réseau social à supprimer.", "Sélection requise", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
