using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using WpfAgendaDatabase.Service.API; 

namespace WpfAgendaDatabase.Service.DAO
{
    public class DAO_GoogleCalendar
    {
        private static string[] Scopes = { CalendarService.Scope.Calendar }; // Étendue de l'API Google Calendar
        private static string ApplicationName = "WpfAgendaDatabase"; // Nom de l'application

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
                new FileDataStore("token.json", true)); // Stockez les jetons d'accès dans un fichier

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
                throw new ArgumentNullException(nameof(service), "Le service de calendrier n'est pas initialisé."); // Gestion des erreurs
            }

            EventsResource.ListRequest request = service.Events.List("primary"); // Obtenez les événements de l'agenda principal
            request.TimeMin = DateTime.Now; 
            request.ShowDeleted = false; 
            request.SingleEvents = true; 
            request.MaxResults = 10; // Limiter le nombre d'événements à 10
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime; // Trier les événements par heure de début

            try
            {
                var events = await request.ExecuteAsync(); // Exécutez la requête pour obtenir les événements
                return events.Items.ToList(); // Retourne la liste des événements
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Affichez l'erreur dans la console
                return new List<Google.Apis.Calendar.v3.Data.Event>(); // Retourne une liste vide en cas d'erreur
            }
        }

        public static async Task DeleteEventAsync(CalendarService service, string eventId) // Méthode pour supprimer un événement
        {
            try
            {
                await service.Events.Delete("primary", eventId).ExecuteAsync(); // Supprimez l'événement avec l'ID spécifié
            }
            catch (Google.GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound) // Gestion des erreurs 404
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