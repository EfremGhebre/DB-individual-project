using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NewAgeHS.Models;

public partial class NewAgeHighSchoolContext : DbContext
{
    public NewAgeHighSchoolContext()
    {
    }

    public NewAgeHighSchoolContext(DbContextOptions<NewAgeHighSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<ReportCard> ReportCards { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=NewAgeHighSchool;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassName);

            entity.ToTable("Class");

            entity.Property(e => e.ClassName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.FkemployeeId).HasColumnName("FKEmpID");

            entity.HasOne(d => d.Fkemp).WithMany(p => p.Classes)
                .HasForeignKey(d => d.FkemployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_Staff");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D718797872F69");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(30);
            entity.Property(e => e.FkemployeeId).HasColumnName("FKEmployeeID");

            entity.HasOne(d => d.Fkemp).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkemployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Staff");
        });

        modelBuilder.Entity<ReportCard>(entity =>
        {
            entity.HasKey(e => e.ReportId);

            entity.ToTable("ReportCard");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.CompletionDate).HasColumnType("date");
            entity.Property(e => e.FkstudId).HasColumnName("FKStudID");
            entity.Property(e => e.SubjectName).HasMaxLength(50);

            entity.HasOne(d => d.Fkstudent).WithMany(p => p.ReportCards)
                .HasForeignKey(d => d.FkstudId)
                .HasConstraintName("FK__ReportCar__FKPer__5DCAEF64");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Staff__AF2DBA79AD4BDA2D");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(30);
            entity.Property(e => e.EmploymentDate).HasColumnType("date");
            entity.Property(e => e.Salary).HasColumnType("salary");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudId).HasName("PK__Students__AA2FFB8510E92956");

            entity.Property(e => e.StudId)
                .ValueGeneratedOnAdd()
                .HasColumnName("StudID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.FkclassName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("FKClassName");
            entity.Property(e => e.FkcourseId).HasColumnName("FKCourseID");
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.FkclassNameNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkclassName)
                .HasConstraintName("FK_Students_Class");

            entity.HasOne(d => d.Fkcourse).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkcourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Course");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
