using System;
using System.Collections.Generic;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class Task
{
    public int IdTasks { get; set; }

    public string Nom { get; set; } = null!;

    public string? Tips { get; set; }

    public sbyte? Check { get; set; }

    public int ToDoListIdToDoList { get; set; }

    public int ToDoListIdentitéIdtable1 { get; set; }

    public int ToDoListIdentitéStatusIdStatus { get; set; }

    public virtual ToDoList ToDoList { get; set; } = null!;
}
