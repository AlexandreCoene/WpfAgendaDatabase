using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WpfAgendaDatabase.Agenda_AlexDB;

public partial class AgendaAlexContext : DbContext
{
    public AgendaAlexContext()
    {
    }

    public AgendaAlexContext(DbContextOptions<AgendaAlexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Identite> Identites { get; set; }

    public virtual DbSet<SocialMedium> SocialMedia { get; set; }

    public virtual DbSet<SocialProfil> SocialProfils { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Tache> Taches { get; set; }

    public virtual DbSet<ToDoList> ToDoLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=agenda_alex", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Identite>(entity =>
        {
            entity.HasKey(e => e.Idtable1).HasName("PRIMARY");

            entity.ToTable("identite");

            entity.Property(e => e.Idtable1).HasColumnName("idtable1");
            entity.Property(e => e.Adresse).HasMaxLength(45);
            entity.Property(e => e.DateDeNaissance).HasColumnName("Date de naissance");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.Nom).HasMaxLength(45);
            entity.Property(e => e.Numero).HasMaxLength(45);
            entity.Property(e => e.Prenom).HasMaxLength(45);
            entity.Property(e => e.Relation).HasColumnType("enum('Famille','Travail','Ami')");
            entity.Property(e => e.Sexe).HasColumnType("enum('M','F')");
            entity.Property(e => e.VilleDeNaissance)
                .HasMaxLength(45)
                .HasColumnName("Ville de naissance");
        });

        modelBuilder.Entity<SocialMedium>(entity =>
        {
            entity.HasKey(e => e.IdSocialMedia).HasName("PRIMARY");

            entity.ToTable("social media");

            entity.Property(e => e.IdSocialMedia).HasColumnName("idSocial Media");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<SocialProfil>(entity =>
        {
            entity.HasKey(e => new { e.IdSocialProfils, e.IdentitéIdtable1, e.IdentitéStatusIdStatus, e.SocialMediaIdSocialMedia })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

            entity.ToTable("social profils");

            entity.HasIndex(e => new { e.IdentitéIdtable1, e.IdentitéStatusIdStatus }, "fk_Social Profils_Identité1_idx");

            entity.HasIndex(e => e.SocialMediaIdSocialMedia, "fk_Social Profils_Social Media1_idx");

            entity.Property(e => e.IdSocialProfils)
                .ValueGeneratedOnAdd()
                .HasColumnName("idSocial Profils");
            entity.Property(e => e.IdentitéIdtable1).HasColumnName("Identité_idtable1");
            entity.Property(e => e.IdentitéStatusIdStatus).HasColumnName("Identité_Status_idStatus");
            entity.Property(e => e.SocialMediaIdSocialMedia).HasColumnName("Social Media_idSocial Media");

            entity.HasOne(d => d.IdentitéIdtable1Navigation).WithMany(p => p.SocialProfils)
                .HasForeignKey(d => d.IdentitéIdtable1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Social Profils_Identité1");

            entity.HasOne(d => d.SocialMediaIdSocialMediaNavigation).WithMany(p => p.SocialProfils)
                .HasForeignKey(d => d.SocialMediaIdSocialMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Social Profils_Social Media1");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => new { e.IdStatus, e.IdentitéIdtable1 })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("status");

            entity.HasIndex(e => e.IdentitéIdtable1, "fk_status_identité1_idx");

            entity.Property(e => e.IdStatus)
                .ValueGeneratedOnAdd()
                .HasColumnName("idStatus");
            entity.Property(e => e.IdentitéIdtable1).HasColumnName("identité_idtable1");
            entity.Property(e => e.Status1)
                .HasColumnType("enum('Famille','Ami','Travail')")
                .HasColumnName("Status");

            entity.HasOne(d => d.IdentitéIdtable1Navigation).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.IdentitéIdtable1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status_identité1");
        });

        modelBuilder.Entity<Tache>(entity =>
        {
            entity.HasKey(e => new { e.IdTasks, e.ToDoListIdToDoList })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("tache");

            entity.HasIndex(e => e.ToDoListIdToDoList, "fk_tasks_to do list1_idx");

            entity.Property(e => e.IdTasks)
                .ValueGeneratedOnAdd()
                .HasColumnName("idTasks");
            entity.Property(e => e.ToDoListIdToDoList).HasColumnName("to do list_idTo do list");
            entity.Property(e => e.Nom).HasMaxLength(45);
            entity.Property(e => e.Tips).HasMaxLength(45);

            entity.HasOne(d => d.ToDoListIdToDoListNavigation).WithMany(p => p.Taches)
                .HasForeignKey(d => d.ToDoListIdToDoList)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tasks_to do list1");
        });

        modelBuilder.Entity<ToDoList>(entity =>
        {
            entity.HasKey(e => e.IdToDoList).HasName("PRIMARY");

            entity.ToTable("to do list");

            entity.Property(e => e.IdToDoList).HasColumnName("idTo do list");
            entity.Property(e => e.Date).HasMaxLength(45);
            entity.Property(e => e.DateFin)
                .HasMaxLength(45)
                .HasColumnName("Date fin");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Titre).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
