using System;
using System.Collections.Generic;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class Identité
{
    public int Idtable1 { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string? Adresse { get; set; }

    public string? Numero { get; set; }

    public string? Email { get; set; }

    public string? Sexe { get; set; }

    public string? DateDeNaissance { get; set; }

    public string? VilleDeNaissance { get; set; }

    public virtual ICollection<SocialProfil> SocialProfils { get; set; } = new List<SocialProfil>();

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();

    public virtual ICollection<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();
}
