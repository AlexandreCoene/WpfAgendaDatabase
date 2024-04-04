using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using WpfAgendaDatabase.Service.API; // Si vos modèles sont dans cet espace de noms

namespace WpfAgendaDatabase.Service.DAO
{
    public class DAO_GoogleCalendar
    {
        private static string[] Scopes = { CalendarService.Scope.Calendar };
        private static string ApplicationName = "WpfAgendaDatabase";

        // Méthode pour obtenir le service de calendrier
        public static async Task<CalendarService> GetCalendarServiceAsync()
        {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "202147414972-jb80gi3pjfm33pvnvfiaemeqmgqld2b2.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore("token.json", true));

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        // Méthode pour obtenir les événements du calendrier
        public static async Task<List<Google.Apis.Calendar.v3.Data.Event>> GetEventsAsync(CalendarService service)
        {
            // Assurez-vous que le service est déjà initialisé
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service), "Le service de calendrier n'est pas initialisé.");
            }

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            try
            {
                var events = await request.ExecuteAsync();
                return events.Items.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Google.Apis.Calendar.v3.Data.Event>();
            }
        }

        public static async Task DeleteEventAsync(CalendarService service, string eventId)
        {
            try
            {
                await service.Events.Delete("primary", eventId).ExecuteAsync();
            }
            catch (Google.GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"L'événement avec l'ID {eventId} n'existe pas.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression de l'événement: {ex.Message}");
            }
        }


    }
}