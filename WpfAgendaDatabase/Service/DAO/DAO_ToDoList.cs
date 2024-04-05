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
        private readonly AgendaAlexContext _context; // Contexte de la base de données

        public DAO_ToDoList()
        {
            _context = new AgendaAlexContext(); // Initialisation du contexte
        }

        //--------------------------------- ToDoList ---------------------------------//

        public List<ToDoList> GetAllToDoLists() // Récupérer toutes les ToDoLists
        {
            return _context.ToDoLists // Récupérer toutes les ToDoLists
                .Include(tdl => tdl.Taches) // Inclure les Taches liées
                .ToList(); // Récupérer la liste
        }

        public void AddToDoList(ToDoList toDoList) // Ajouter une ToDoList
        {
            using (var context = new AgendaAlexContext())
            {
                context.ToDoLists.Add(toDoList); 
                context.SaveChanges();
            }
        }

        public void UpdateToDoList(ToDoList toDoList) // Mettre à jour une ToDoList
        {
            _context.ToDoLists.Update(toDoList); 
            _context.SaveChanges();
        }

        public void DeleteToDoList(int toDoListId) // Supprimer une ToDoList
        {
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


        public void AddTask(Tache task) // Ajouter une tâche
        {
            _context.Taches.Add(task);
            _context.SaveChanges();
        }

        public List<Tache> GetTachesFromToDoList(int toDoListId) // Récupérer les tâches d'une liste ToDoList
        {
            return _context.Taches
                .Where(tache => tache.ToDoListIdToDoList == toDoListId) // Filtrer par toDoListId
                .ToList(); // Récupérer la liste de tâches
        }

        public void DeleteTask(int taskId) // Supprimer une tâche
        {
            var taskToDelete = _context.Taches.FirstOrDefault(t => t.IdTasks == taskId); // Récupérer la tâche à supprimer
            if (taskToDelete != null)
            {
                _context.Taches.Remove(taskToDelete); // Supprimer la tâche
                _context.SaveChanges();
            }
        }

    }
}
