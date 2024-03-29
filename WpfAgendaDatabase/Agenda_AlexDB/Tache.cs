using System;
using System.Collections.Generic;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class Tache
{
    public int IdTasks { get; set; }

    public string Nom { get; set; } = null!;

    public string? Tips { get; set; }

    public sbyte? Check { get; set; }

    public int ToDoListIdToDoList { get; set; }

    public virtual ToDoList ToDoListIdToDoListNavigation { get; set; } = null!;
}
