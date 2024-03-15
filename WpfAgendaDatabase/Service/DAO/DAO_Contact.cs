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
        public List<Identité> GetAllContacts()
        {
            using (var context = new AgendaAlexContext())
            {
                return context.Identités.ToList();
            }
        }

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

        //public Status GetStatusByName(string name)
        //{
        //    using (var context = new AgendaAlexContext())
        //    {
        //        return context.Statuses.FirstOrDefault(s => s.Status1 == name);
        //    }
        //}



        //public List<Identité> LoadAllContacts_Status()
        //{
        //    using (var context = new AgendaAlexContext())
        //    {
        //        var allcont = context.Identités.Include (c => c.Statuses).ToList();
        //        return allcont;
        //    }
        //}

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



    }
}
