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
using WpfAgendaDatabase.Service.DAO;
using WpfAgendaDatabase.Agenda_AlexDB;

namespace WpfAgendaDatabase.View
{
    /// <summary>
    /// Logique d'interaction pour ViewModifierEvent.xaml
    /// </summary>
    public partial class ViewModifierEvent : UserControl
    {
        private Event existingEvent;

        public ViewModifierEvent()
        {
            InitializeComponent();
            InitializeTimePickers();
        }

        private void InitializeTimePickers()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                StartHourEdit.Items.Add(hour.ToString("00"));
                EndHourEdit.Items.Add(hour.ToString("00"));
            }

            for (int minute = 0; minute < 60; minute += 5) // Incrémentation par 5 minutes
            {
                StartMinuteEdit.Items.Add(minute.ToString("00"));
                EndMinuteEdit.Items.Add(minute.ToString("00"));
            }

            StartHourEdit.SelectedIndex = 0;
            StartMinuteEdit.SelectedIndex = 0;
            EndHourEdit.SelectedIndex = 0;
            EndMinuteEdit.SelectedIndex = 0;
        }

        public void LoadEvent(Event eventToEdit)
        {
            existingEvent = eventToEdit;

            // Update the UI with the event's data
            EventTitleEdit.Text = existingEvent.Summary;
            EventDescriptionEdit.Text = existingEvent.Description;

            if (existingEvent.Start.DateTime.HasValue)
            {
                var start = existingEvent.Start.DateTime.Value;
                StartDateEdit.SelectedDate = start;
                StartHourEdit.SelectedValue = start.Hour.ToString("00");
                StartMinuteEdit.SelectedValue = start.Minute.ToString("00");
            }

            if (existingEvent.End.DateTime.HasValue)
            {
                var end = existingEvent.End.DateTime.Value;
                EndDateEdit.SelectedDate = end;
                EndHourEdit.SelectedValue = end.Hour.ToString("00");
                EndMinuteEdit.SelectedValue = end.Minute.ToString("00");
            }
        }


        private async void SaveModifiedEventButton_Click(object sender, RoutedEventArgs e)
        {
            var service = await DAO_GoogleCalendar.GetCalendarServiceAsync();

            // Récupération des dates et heures à partir de l'interface utilisateur
            var startDate = StartDateEdit.SelectedDate.Value;
            var startHour = int.Parse(StartHourEdit.SelectedItem.ToString());
            var startMinute = int.Parse(StartMinuteEdit.SelectedItem.ToString());
            var startTime = new TimeSpan(startHour, startMinute, 0);

            var endDate = EndDateEdit.SelectedDate.Value;
            var endHour = int.Parse(EndHourEdit.SelectedItem.ToString());
            var endMinute = int.Parse(EndMinuteEdit.SelectedItem.ToString());
            var endTime = new TimeSpan(endHour, endMinute, 0);

            // Mise à jour des propriétés de l'événement avec les nouvelles valeurs
            existingEvent.Summary = EventTitleEdit.Text;
            existingEvent.Description = EventDescriptionEdit.Text;
            existingEvent.Start.DateTime = startDate.Add(startTime);
            existingEvent.End.DateTime = endDate.Add(endTime);

            // Vous devriez également définir le fuseau horaire si nécessaire
            existingEvent.Start.TimeZone = "Europe/Paris"; // Par exemple: "America/Los_Angeles"
            existingEvent.End.TimeZone = "Europe/Paris";

            try
            {
                // Mise à jour de l'événement dans le calendrier
                var updatedEvent = await service.Events.Update(existingEvent, "primary", existingEvent.Id).ExecuteAsync();
                MessageBox.Show("L'événement a été mis à jour avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de la mise à jour de l'événement: {ex.Message}");
            }
        }


    }
}
