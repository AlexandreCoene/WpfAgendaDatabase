using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAgendaDatabase.Agenda_AlexDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace WpfAgendaDatabase.Service.DAO
{
    public class DAO_ToDoList
    {
        private readonly AgendaAlexContext _context;

        public DAO_ToDoList()
        {
            _context = new AgendaAlexContext();
        }

        public List<ToDoList> GetAllToDoLists()
        {
            return _context.ToDoLists
                .Include(tdl => tdl.Tasks)
                .ToList();
        }

    }
}
