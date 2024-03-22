using System;
using System.Collections.Generic;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class SocialProfil
{
    public int IdSocialProfils { get; set; }

    public int IdentitéIdtable1 { get; set; }

    public int IdentitéStatusIdStatus { get; set; }

    public int SocialMediaIdSocialMedia { get; set; }

    public virtual Identite IdentitéIdtable1Navigation { get; set; } = null!;

    public virtual SocialMedium SocialMediaIdSocialMediaNavigation { get; set; } = null!;
}
