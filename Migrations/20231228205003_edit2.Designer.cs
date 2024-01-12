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
    [Migration("20231228205003_edit2")]
    partial class edit2
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

                    b.Property<int>("FkemployeeId")
                        .HasColumnType("int")
                        .HasColumnName("FKEmpID");

                    b.HasKey("ClassName");

                    b.HasIndex("FkemployeeId");

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

                    b.Property<int>("FkemployeeId")
                        .HasColumnType("int")
                        .HasColumnName("FKEmployeeID");

                    b.HasKey("CourseId")
                        .HasName("PK__Course__C92D718797872F69");

                    b.HasIndex("FkemployeeId");

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

                    b.Property<int?>("FkstudId")
                        .HasColumnType("int")
                        .HasColumnName("FKStudentID");

                    b.Property<int?>("Grades")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ReportId");

                    b.HasIndex("FkstudId");

                    b.ToTable("ReportCard", (string)null);
                });

            modelBuilder.Entity("NewAgeHS.Models.Staff", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

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

                    b.HasKey("EmployeeId")
                        .HasName("PK__Staff__AF2DBA79AD4BDA2D");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("NewAgeHS.Models.Student", b =>
                {
                    b.Property<int>("StudId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StudID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudId"));

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

                    b.HasKey("StudId")
                        .HasName("PK__Students__AA2FFB8510E92956");

                    b.HasIndex("FkclassName");

                    b.HasIndex("FkcourseId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("NewAgeHS.Models.Class", b =>
                {
                    b.HasOne("NewAgeHS.Models.Staff", "Fkemp")
                        .WithMany("Classes")
                        .HasForeignKey("FkemployeeId")
                        .IsRequired()
                        .HasConstraintName("FK_Class_Staff");

                    b.Navigation("Fkemp");
                });

            modelBuilder.Entity("NewAgeHS.Models.Course", b =>
                {
                    b.HasOne("NewAgeHS.Models.Staff", "Fkemp")
                        .WithMany("Courses")
                        .HasForeignKey("FkemployeeId")
                        .IsRequired()
                        .HasConstraintName("FK_Course_Staff");

                    b.Navigation("Fkemp");
                });

            modelBuilder.Entity("NewAgeHS.Models.ReportCard", b =>
                {
                    b.HasOne("NewAgeHS.Models.Student", "Fkstudent")
                        .WithMany("ReportCards")
                        .HasForeignKey("FkstudId")
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
