using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAgendaDatabase.Agenda_AlexDB;

namespace WpfAgendaDatabase.Service.DAO
{
    public class DAO_Contact
    {

        //----------------------------------------- Contact ----------------------------------------------
        public void Ajouter(Identite contact) // Ajouter un contact
        {
            using (var context = new AgendaAlexContext())
            {
                context.Identites.Add(contact);
                context.SaveChanges();
            }
        }

        public List<Identite> LoadAllContacts() // Charger tous les contacts
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identites.ToList();
            }
        }

        public List<Identite> LoadAllContacts_Status() // Charger tous les contacts avec leur statut
        {
            using (var context = new AgendaAlexContext())
            {
                var allcont = context.Identites.Include(c => c.Statuses).ToList();
                return allcont;
            }
        }

        public void DeleteContact(Identite contact) // Supprimer un contact 
        {
            using (var context = new AgendaAlexContext())
            {
                var item = context.Identites
                    .FirstOrDefault(i => i.Idtable1 == contact.Idtable1);

                if (item != null)
                {
                    context.Identites.Remove(item); 
                    context.SaveChanges();
                }
            }
        }

        public List<Identite> GetContactsByRelation(string relation)  // Récupérer les contacts par relation (Famille, Travail, Ami)
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identites
                              .Where(i => i.Relation == relation)
                              .ToList();
            }
        }

        public List<Identite> RechercherContacts(string searchTerm) // Rechercher des contacts
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identites
                              .Where(i => i.Nom.Contains(searchTerm) || i.Prenom.Contains(searchTerm) || i.Numero.Contains(searchTerm) || i.Email.Contains(searchTerm))
                              .ToList();
            }
        }

        //----------------------------------------- Statut DB ----------------------------------------------

        public bool checkdatabaseconnect() // Vérifier la connexion à la base de données
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Database.CanConnect();
            }
        }

        //-------------------------------------- Social Media ------------------------------------------
        public List<SocialProfil> GetSocialProfiles(int contactId) // Récupérer les profils sociaux d'un contact
        {
            using (var context = new AgendaAlexContext())
            {
                return context.SocialProfils
                    .Where(p => p.IdentitéIdtable1 == contactId)
                    .Include(p => p.SocialMediaIdSocialMediaNavigation)
                    .ToList();
            }
        }

        public void AddSocialProfil(SocialProfil profile, SocialMedium socialMedia) // Ajouter un profil social
        {
            using (var context = new AgendaAlexContext())
            {
                context.SocialMedia.Add(socialMedia);
                context.SaveChanges();

                profile.SocialMediaIdSocialMedia = socialMedia.IdSocialMedia;
                context.SocialProfils.Add(profile);
                context.SaveChanges();
            }
        }

        public void DeleteSocialProfile(SocialProfil profile) // Supprimer un profil social
        {
            using (var context = new AgendaAlexContext())
            {
                var existingProfile = context.SocialProfils
                    .FirstOrDefault(p => p.IdSocialProfils == profile.IdSocialProfils);

                if (existingProfile != null)
                {
                    context.SocialProfils.Remove(existingProfile);
                    context.SaveChanges();
                }
            }
        }


    }
}
