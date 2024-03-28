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

        public void AddToDoList(ToDoList toDoList)
        {
            using (var context = new AgendaAlexContext())
            {
                context.ToDoLists.Add(toDoList);
                context.SaveChanges();
            }
        }

        public void UpdateToDoList(ToDoList toDoList)
        {
            _context.ToDoLists.Update(toDoList);
            _context.SaveChanges();
        }

        public void DeleteToDoList(int idToDoList)
        {
            var toDoList = _context.ToDoLists.Find(idToDoList);
            if (toDoList != null)
            {
                _context.ToDoLists.Remove(toDoList);
                _context.SaveChanges();
            }
        }



        //--------------------------------- Task ---------------------------------//

        //public void AddTask(Task task)
        //{
        //    _context.Tasks.Add(task);
        //    _context.SaveChanges();
        //}

        //public void UpdateTask(Task task)
        //{
        //    _context.Tasks.Update(task);
        //    _context.SaveChanges();
        //}

        //public void DeleteTask(Task task)
        //{
        //    _context.Tasks.Remove(task);
        //    _context.SaveChanges();
        //}

        //public List<Task> GetAllTasks()
        //{
        //    return _context.Tasks.ToList();
        //}
    }
}
