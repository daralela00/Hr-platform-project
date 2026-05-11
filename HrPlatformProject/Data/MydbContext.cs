using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace HrPlatformProject.Models;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Idcandidates).HasName("PRIMARY");

            entity.ToTable("candidates");

            entity.Property(e => e.Idcandidates).HasColumnName("idcandidates");
            entity.Property(e => e.ContactNumber).HasColumnName("contactNumber");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.HasIndex(e => e.Email)
                .IsUnique();
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Idskills).HasName("PRIMARY");

            entity.ToTable("skills");

            entity.Property(e => e.Idskills).HasColumnName("idskills");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");

            entity.HasMany(d => d.CandidatesIdcandidates).WithMany(p => p.SkillsIdskills)
                .UsingEntity<Dictionary<string, object>>(
                    "Candidateskill",
                    r => r.HasOne<Candidate>().WithMany()
                        .HasForeignKey("CandidatesIdcandidates")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_skills_has_candidates_candidates1"),
                    l => l.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillsIdskills")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_skills_has_candidates_skills"),
                    j =>
                    {
                        j.HasKey("SkillsIdskills", "CandidatesIdcandidates")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("candidateskills");
                        j.HasIndex(new[] { "CandidatesIdcandidates" }, "fk_skills_has_candidates_candidates1_idx");
                        j.HasIndex(new[] { "SkillsIdskills" }, "fk_skills_has_candidates_skills_idx");
                        j.IndexerProperty<int>("SkillsIdskills").HasColumnName("skills_idskills");
                        j.IndexerProperty<int>("CandidatesIdcandidates").HasColumnName("candidates_idcandidates");
                    });

            entity.HasIndex(e => e.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Candidate>().HasData(
            new Candidate
            {
                Idcandidates = 1,
                Name = "Marko Markovic",
                DateOfBirth = new DateOnly(2000, 3, 15),
                ContactNumber = 0641234567,
                Email = "marko@gmail.com"
            },
            new Candidate
            {
                Idcandidates = 2,
                Name = "Ana Jovanovic",
                DateOfBirth = new DateOnly(1998, 7, 22),
                ContactNumber = 0659876543,
                Email = "ana@gmail.com"
            }
        );

        modelBuilder.Entity<Skill>().HasData(
        new Skill { Idskills = 1, Name = "C# programming" },
        new Skill { Idskills = 2, Name = "Java programming" },
        new Skill { Idskills = 3, Name = "Database design" },
        new Skill { Idskills = 4, Name = "English language" },
        new Skill { Idskills = 5, Name = "Russian language" },
        new Skill { Idskills = 6, Name = "German language" }  
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
