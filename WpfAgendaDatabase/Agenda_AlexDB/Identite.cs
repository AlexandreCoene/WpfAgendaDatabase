using System;
using System.Collections.Generic;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class Identite
{
    public int Idtable1 { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string? Adresse { get; set; }

    public string? Numero { get; set; }

    public string? Email { get; set; }

    public string? Sexe { get; set; }

    public DateOnly? DateDeNaissance { get; set; }

    public string? VilleDeNaissance { get; set; }

    public string Relation { get; set; } = null!;

    public virtual ICollection<SocialProfil> SocialProfils { get; set; } = new List<SocialProfil>();

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}
