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

        public void Ajouter(Identité contact)
        {
            using (var context = new AgendaAlexContext())
            {
                context.Identités.Add(contact);
                context.SaveChanges();
            }
        }

        public List<Identité> LoadAllContacts()
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identités.ToList();
            }
        }

        public List<Identité> LoadAllContacts_Status()
        {
            using (var context = new AgendaAlexContext())
            {
                var allcont = context.Identités.Include(c => c.Statuses).ToList();
                return allcont;
            }
        }

        public void DeleteContact(Identité contact)
        {
            using (var context = new AgendaAlexContext())
            {
                var item = context.Identités
                    .FirstOrDefault(i => i.Idtable1 == contact.Idtable1);

                if (item != null)
                {
                    context.Identités.Remove(item); 
                    context.SaveChanges();
                }
            }
        }

        public List<Identité> GetContactsByRelation(string relation) 
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identités
                              .Where(i => i.Relation == relation)
                              .ToList();
            }
        }

        public List<Identité> RechercherContacts(string searchTerm)
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identités
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
