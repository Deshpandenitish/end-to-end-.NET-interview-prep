using System;
using System.Collections.Generic;
using DotNet_Core_Prep_Samples.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Core_Prep_Samples.Data;

public partial class TrialsContext : DbContext
{
    public TrialsContext()
    {
    }

    public TrialsContext(DbContextOptions<TrialsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=3410-TI13229;Initial Catalog=Trials;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__Departme__014881AE03AF205C");

            entity.Property(e => e.DeptId).ValueGeneratedNever();
            entity.Property(e => e.DeptName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB99564AD407");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
            entity.Property(e => e.EmpName).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Dept).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK__Employees__DeptI__398D8EEE");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjId).HasName("PK__Projects__16212A1C46145A8F");

            entity.Property(e => e.ProjId).ValueGeneratedNever();
            entity.Property(e => e.ProjName).HasMaxLength(50);

            entity.HasOne(d => d.Dept).WithMany(p => p.Projects)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK__Projects__DeptId__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
