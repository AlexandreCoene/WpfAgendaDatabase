using System;
using System.Collections.Generic;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class Status
{
    public int IdStatus { get; set; }

    public string Status1 { get; set; } = null!;

    public int IdentitéIdtable1 { get; set; }

    public virtual Identite IdentitéIdtable1Navigation { get; set; } = null!;
}
