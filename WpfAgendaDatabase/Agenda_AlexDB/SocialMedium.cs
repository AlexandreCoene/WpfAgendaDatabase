using System;
using System.Collections.Generic;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class SocialMedium
{
    public int IdSocialMedia { get; set; }

    public string Name { get; set; } = null!;

    public string? Url { get; set; }

    public virtual ICollection<SocialProfil> SocialProfils { get; set; } = new List<SocialProfil>();
}
