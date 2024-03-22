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

        public void Ajouter(Identite contact)
        {
            using (var context = new AgendaAlexContext())
            {
                context.Identites.Add(contact);
                context.SaveChanges();
            }
        }

        public List<Identite> LoadAllContacts()
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identites.ToList();
            }
        }

        public List<Identite> LoadAllContacts_Status()
        {
            using (var context = new AgendaAlexContext())
            {
                var allcont = context.Identites.Include(c => c.Statuses).ToList();
                return allcont;
            }
        }

        public void DeleteContact(Identite contact)
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

        public List<Identite> GetContactsByRelation(string relation) 
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identites
                              .Where(i => i.Relation == relation)
                              .ToList();
            }
        }

        public List<Identite> RechercherContacts(string searchTerm)
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identites
                              .Where(i => i.Nom.Contains(searchTerm) || i.Prenom.Contains(searchTerm) || i.Numero.Contains(searchTerm) || i.Email.Contains(searchTerm))
                              .ToList();
            }
        }


        public bool checkdatabaseconnect()
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Database.CanConnect();
            }
        }


    }
}
