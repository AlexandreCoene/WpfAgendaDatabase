using Google.Apis.Calendar.v3.Data;
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
    /// Logique d'interaction pour ViewAjouterEvent.xaml
    /// </summary>
    public partial class ViewAjouterEvent : UserControl
    {
        public ViewAjouterEvent()
        {
            InitializeComponent();
            InitializeTimePickers();
        }

        private void InitializeTimePickers()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                StartHourComboBox.Items.Add(hour.ToString("00"));
                EndHourComboBox.Items.Add(hour.ToString("00"));
            }

            for (int minute = 0; minute < 60; minute += 5) // Incrémentation par 5 minutes
            {
                StartMinuteComboBox.Items.Add(minute.ToString("00"));
                EndMinuteComboBox.Items.Add(minute.ToString("00"));
            }

            StartHourComboBox.SelectedIndex = 0;
            StartMinuteComboBox.SelectedIndex = 0;
            EndHourComboBox.SelectedIndex = 0;
            EndMinuteComboBox.SelectedIndex = 0;
        }

        private async void SaveEventButton_Click(object sender, RoutedEventArgs e)
        {
            // Assurez-vous que le service est correctement initialisé et authentifié.
            var service = await DAO_GoogleCalendar.GetCalendarServiceAsync();

            var startDate = StartDatePicker.SelectedDate.Value;
            var startHour = int.Parse((string)StartHourComboBox.SelectedItem);
            var startMinute = int.Parse((string)StartMinuteComboBox.SelectedItem);
            var startTime = new TimeSpan(startHour, startMinute, 0);

            var endDate = EndDatePicker.SelectedDate.Value;
            var endHour = int.Parse((string)EndHourComboBox.SelectedItem);
            var endMinute = int.Parse((string)EndMinuteComboBox.SelectedItem);
            var endTime = new TimeSpan(endHour, endMinute, 0);

            // Créez l'événement avec les informations collectées
            var newEvent = new Event
            {
                Summary = EventTitleTextBox.Text,
                Description = EventDescriptionTextBox.Text, // Ajoutez ceci pour la description
                Start = new EventDateTime() { DateTime = startDate.Add(startTime), TimeZone = "Europe/Paris" },
                End = new EventDateTime() { DateTime = endDate.Add(endTime), TimeZone = "Europe/Paris" }
            };


            try
            {
                // Insérez l'événement dans le calendrier.
                var createdEvent = await service.Events.Insert(newEvent, "primary").ExecuteAsync();
                MessageBox.Show($"Événement créé : {createdEvent.HtmlLink}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de l'ajout de l'événement: {ex.Message}");
            }
        }


    }

}
