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
                .Include(tdl => tdl.Taches)
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

        public void DeleteToDoList(int toDoListId)
        {
            // Find the toDoList with its related Taches
            var toDoList = _context.ToDoLists
                .Include(tdl => tdl.Taches)
                .FirstOrDefault(tdl => tdl.IdToDoList == toDoListId);

            if (toDoList != null)
            {
                // Remove each Tache related to the ToDoList
                foreach (var tache in toDoList.Taches.ToList())
                {
                    _context.Taches.Remove(tache);
                }

                // Remove the ToDoList itself
                _context.ToDoLists.Remove(toDoList);
                _context.SaveChanges();
            }
        }


        //--------------------------------- Task ---------------------------------//


        public void AddTask(Tache task)
        {
            _context.Taches.Add(task);
            _context.SaveChanges();
        }

        public List<Tache> GetTachesFromToDoList(int toDoListId)
        {
            return _context.Taches
                .Where(tache => tache.ToDoListIdToDoList == toDoListId)
                .ToList();
        }

        public void DeleteTask(int taskId)
        {
            var taskToDelete = _context.Taches.FirstOrDefault(t => t.IdTasks == taskId);
            if (taskToDelete != null)
            {
                _context.Taches.Remove(taskToDelete);
                _context.SaveChanges();
            }
        }

    }
}
