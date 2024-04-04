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
using WpfAgendaDatabase.Service.DAO;
using Google.Apis.Calendar.v3.Data;
using System.Collections.ObjectModel;
using WpfAgendaDatabase.Service.API;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using System.IO;
using WpfAgendaDatabase.Agenda_AlexDB;

namespace WpfAgendaDatabase.View
{
    public partial class ViewCalendrier : UserControl
    {
        public ObservableCollection<Event> Events { get; set; } = new ObservableCollection<Event>();


        public ViewCalendrier()
        {
            InitializeComponent();
            this.DataContext = this;
            LoadEventsAsync();
        }

        private async void LoadEventsAsync()
        {
            try
            {
                var service = await DAO_GoogleCalendar.GetCalendarServiceAsync();
                var request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                var events = await request.ExecuteAsync();

                foreach (var eventItem in events.Items)
                {
                    Events.Add(eventItem);
                }
            }
            catch (Exception ex)
            {
                // Gérer l'erreur, par exemple en affichant un message
                Console.WriteLine($"Erreur lors du chargement des événements: {ex.Message}");
            }
        }

        private void Button_Click_Modify_Event(object sender, RoutedEventArgs e)
        {
            // Obtenez l'événement sélectionné de la ListView.
            var selectedEvent = EventsListView.SelectedItem as Event;

            // Assurez-vous qu'un événement a été sélectionné.
            if (selectedEvent == null)
            {
                MessageBox.Show("Veuillez sélectionner un événement à modifier.");
                return;
            }

            // Créez la vue de modification et chargez l'événement sélectionné.
            var viewModifierEvent = new ViewModifierEvent();
            viewModifierEvent.LoadEvent(selectedEvent);

            // Affichez la vue de modification dans le ContentControl principal.
            MainContent.Content = viewModifierEvent;
        }


        private void Button_Click_Add_Event(object sender, RoutedEventArgs e)
        {

            MainContent.Content = new ViewAjouterEvent();
             // Assurez-vous que c'est visible
        }


        private async void Button_Click_Dell_Event(object sender, RoutedEventArgs e)
        {
            var selectedEvent = EventsListView.SelectedItem as Event;
            if (selectedEvent != null)
            {
                var service = await DAO_GoogleCalendar.GetCalendarServiceAsync();
                await DAO_GoogleCalendar.DeleteEventAsync(service, selectedEvent.Id);
                Events.Remove(selectedEvent);

                // Rafraîchissement de la liste des événements
                Events.Clear();
                LoadEventsAsync();
            }
        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tokenPath = "token.json"; // ou le chemin d'accès où sont stockées les données de token
                if (Directory.Exists(tokenPath))
                {
                    Directory.Delete(tokenPath, true);
                    MessageBox.Show("Vous avez été déconnecté.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la déconnexion : " + ex.Message);
            }
        }

    }
}