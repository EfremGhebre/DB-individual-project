﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewAgeHS.Models;

#nullable disable

namespace NewAgeHS.Migrations
{
    [DbContext(typeof(NewAgeHighSchoolContext))]
    [Migration("20231228143606_edited")]
    partial class edited
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NewAgeHS.Models.Class", b =>
                {
                    b.Property<string>("ClassName")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.Property<int>("FkempId")
                        .HasColumnType("int")
                        .HasColumnName("FKEmpID");

                    b.HasKey("ClassName");

                    b.HasIndex("FkempId");

                    b.ToTable("Class", (string)null);
                });

            modelBuilder.Entity("NewAgeHS.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("FkempId")
                        .HasColumnType("int")
                        .HasColumnName("FKEmpID");

                    b.HasKey("CourseId")
                        .HasName("PK__Course__C92D718797872F69");

                    b.HasIndex("FkempId");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("NewAgeHS.Models.ReportCard", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReportID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportId"));

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("date");

                    b.Property<int?>("FkstudentId")
                        .HasColumnType("int")
                        .HasColumnName("FKStudentID");

                    b.Property<int?>("Grades")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ReportId");

                    b.HasIndex("FkstudentId");

                    b.ToTable("ReportCard", (string)null);
                });

            modelBuilder.Entity("NewAgeHS.Models.Staff", b =>
                {
                    b.Property<int>("EmpId")
                        .HasColumnType("int")
                        .HasColumnName("EmpID");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("EmpId")
                        .HasName("PK__Staff__AF2DBA79AD4BDA2D");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("NewAgeHS.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("StudentID");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FkclassName")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("FKClassName")
                        .IsFixedLength();

                    b.Property<int>("FkcourseId")
                        .HasColumnType("int")
                        .HasColumnName("FKCourseID");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StudentId")
                        .HasName("PK__Students__AA2FFB8510E92956");

                    b.HasIndex("FkclassName");

                    b.HasIndex("FkcourseId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("NewAgeHS.Models.Class", b =>
                {
                    b.HasOne("NewAgeHS.Models.Staff", "Fkemp")
                        .WithMany("Classes")
                        .HasForeignKey("FkempId")
                        .IsRequired()
                        .HasConstraintName("FK_Class_Staff");

                    b.Navigation("Fkemp");
                });

            modelBuilder.Entity("NewAgeHS.Models.Course", b =>
                {
                    b.HasOne("NewAgeHS.Models.Staff", "Fkemp")
                        .WithMany("Courses")
                        .HasForeignKey("FkempId")
                        .IsRequired()
                        .HasConstraintName("FK_Course_Staff");

                    b.Navigation("Fkemp");
                });

            modelBuilder.Entity("NewAgeHS.Models.ReportCard", b =>
                {
                    b.HasOne("NewAgeHS.Models.Student", "Fkstudent")
                        .WithMany("ReportCards")
                        .HasForeignKey("FkstudentId")
                        .HasConstraintName("FK__ReportCar__FKPer__5DCAEF64");

                    b.Navigation("Fkstudent");
                });

            modelBuilder.Entity("NewAgeHS.Models.Student", b =>
                {
                    b.HasOne("NewAgeHS.Models.Class", "FkclassNameNavigation")
                        .WithMany("Students")
                        .HasForeignKey("FkclassName")
                        .HasConstraintName("FK_Students_Class");

                    b.HasOne("NewAgeHS.Models.Course", "Fkcourse")
                        .WithMany("Students")
                        .HasForeignKey("FkcourseId")
                        .IsRequired()
                        .HasConstraintName("FK_Students_Course");

                    b.Navigation("FkclassNameNavigation");

                    b.Navigation("Fkcourse");
                });

            modelBuilder.Entity("NewAgeHS.Models.Class", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("NewAgeHS.Models.Course", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("NewAgeHS.Models.Staff", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Courses");
                });

            modelBuilder.Entity("NewAgeHS.Models.Student", b =>
                {
                    b.Navigation("ReportCards");
                });
#pragma warning restore 612, 618
        }
    }
}
