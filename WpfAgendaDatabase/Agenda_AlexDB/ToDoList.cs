﻿using System;
using System.Collections.Generic;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class ToDoList
{
    public int IdToDoList { get; set; }

    public string Titre { get; set; } = null!;

    public string? Date { get; set; }

    public string? DateFin { get; set; }

    public string? Description { get; set; }

    public int IdentitéIdtable1 { get; set; }

    public virtual Identité IdentitéIdtable1Navigation { get; set; } = null!;
}
