using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NewAgeHS.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace NewAgeHS.Models;

public partial class ReportCard
{
    public int ReportId { get; set; }

    public string? SubjectName { get; set; }

    public int? Grades { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int? FkstudId { get; set; }

    public virtual Student? Fkstudent { get; set; }

    public void StudentAverageGrade()
    {
        Console.Clear();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();

        var studentGrades = from student in context.Students
                            join reportcard in context.ReportCards on student.StudId equals reportcard.FkstudId
                            select new
                            {
                                StudID = student.StudId,
                                FirstName = student.FirstName,
                                LastName = student.LastName,
                                FkclassName = student.FkclassName,
                                Grades = reportcard.Grades,
                                CompletionDate = reportcard.CompletionDate
                            };

        // Group by student and calculate average grades
        var averageGrades = studentGrades
            .GroupBy(student => new { student.StudID, student.FirstName, student.LastName, student.FkclassName })
            .Select(group => new
            {
                StudID = group.Key.StudID,
                FirstName = group.Key.FirstName,
                LastName = group.Key.LastName,
                FkclassName = group.Key.FkclassName,
                AverageGrade = group.Average(student => student.Grades) // Calculate average grades                
            });

        var table = new Table();

        table.AddColumn("StudID");// Adds columns un the display table 
        table.AddColumn("First name");
        table.AddColumn("Last name");
        table.AddColumn("FKClassName");
        table.AddColumn("Average Grade");

        foreach (var i in averageGrades)
        {
            table.AddRow(
                i.StudID.ToString(),
                i.FirstName,
                i.LastName,
                i.FkclassName,
                i.AverageGrade.ToString()
            );
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to the main menu. ");
        Console.ReadKey();
    }
    public void StudentReportCard()
    {
        Console.Clear();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();

        var studentGrades = from student in context.Students
                            join reportcard in context.ReportCards on student.StudId equals reportcard.FkstudId
                            select new
                            {
                                StudID = student.StudId,
                                FirstName = student.FirstName,
                                LastName = student.LastName,
                                FkclassName = student.FkclassName,
                                SubjectName = reportcard.SubjectName,
                                Grades = reportcard.Grades,
                                CompletionDate = reportcard.CompletionDate,
                            };

        var table = new Table();

        table.AddColumn("StudID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("FkClassName");
        table.AddColumn("SubjectName");
        table.AddColumn("Grades");
        table.AddColumn("CompletionDate");

        foreach (var studentGrade in studentGrades)
        {
            table.AddRow(studentGrade.StudID.ToString(), studentGrade.FirstName,
                              studentGrade.LastName, studentGrade.FkclassName,
                              studentGrade.SubjectName, studentGrade.Grades.ToString(),
                              studentGrade.CompletionDate.ToString());
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to the main menu. ");
        Console.ReadKey();
    }

    public void LastMonthGrades()
    {
        using (var context = new NewAgeHighSchoolContext())
        {
            var currentDate = DateTime.Now;
            var lastMonth = currentDate.AddMonths(-2);

            var lastMonthGrades = from student in context.Students
                                  join ReportCard in context.ReportCards on student.StudId equals ReportCard.FkstudId
                                  where ReportCard.CompletionDate >= lastMonth
                                  && ReportCard.CompletionDate < currentDate.AddMonths(-1)
                                  select new
                                  {
                                      StudID = student.StudId,
                                      FirstName = student.FirstName,
                                      LastName = student.LastName,
                                      SubjectName = ReportCard.SubjectName,
                                      Grades = ReportCard.Grades,
                                      CompletionDate = ReportCard.CompletionDate
                                  };

            var table = new Table();

            table.AddColumn("StudID");
            table.AddColumn("FirstName");
            table.AddColumn("LastName");
            table.AddColumn("SubjectName");
            table.AddColumn("Grades");
            table.AddColumn("CompletionDate");

            foreach (var i in lastMonthGrades)
            {
                table.AddRow(
                    i.StudID.ToString(),
                    i.FirstName,
                    i.LastName,
                    i.SubjectName,
                    i.Grades.ToString(),
                    i.CompletionDate.ToString());
            }

            AnsiConsole.Write(table);
            Console.WriteLine("Press enter to go back to the main menu. ");
            Console.ReadKey();
        }
    }

    public void CourseAverageGrades()
    {
        Console.Clear();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();

        var courseGrades = from student in context.Students
                           join reportcard in context.ReportCards on student.StudId equals reportcard.FkstudId
                           join course in context.Courses on student.FkcourseId equals course.CourseId
                           select new
                           {
                               CourseName = course.CourseName,
                               Grades = reportcard.Grades,
                           };

        // Group by student and calculate average grades in a specific course
        var averageGrades = courseGrades
            .GroupBy(student => new { student.CourseName })
            .Select(group => new
            {
                CourseName = group.Key.CourseName,
                AverageGrade = group.Average(student => student.Grades),
                MaxGrade = group.Max(student => student.Grades),
                MinGrade = group.Min(student => student.Grades)
            });

        var table = new Table();

        table.AddColumn("Course Name");
        table.AddColumn("Average Grade");
        table.AddColumn("Max Grade");
        table.AddColumn("Min Grade");
        foreach (var i in averageGrades)
        {
            table.AddRow(
                i.CourseName,
                i.AverageGrade.ToString(),
                i.MaxGrade.ToString(),
                i.MinGrade.ToString()
            );
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to the main menu. ");
        Console.ReadKey();
    }

    public void RegisterGrades()
    {
        while (true)
        {
            try
            {
                NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();
                var currentDate = DateTime.Now;

                Console.WriteLine("Fill student ID:");
                int studentID = int.Parse(Console.ReadLine());
                Console.WriteLine("Fill subject name:");
                string subjectName = Console.ReadLine();
                Console.WriteLine("Fill course ID:");
                int FkcourseId = int.Parse(Console.ReadLine());
                Console.WriteLine("Fill grades 1-5 for completed course\nF for incomplete or failed:");
                var studentGrades = int.Parse(Console.ReadLine());
                Console.WriteLine("Fill teacher ID:");
                int teacherID = int.Parse(Console.ReadLine());

                ReportCard setGrade = new ReportCard();
                {
                    setGrade.FkstudId = studentID;
                    setGrade.SubjectName = subjectName;
                    setGrade.Grades = studentGrades;
                    setGrade.CompletionDate = currentDate;
                };
                context.ReportCards.Add(setGrade);
                context.SaveChanges();

                Console.WriteLine("Student's grade is registered. ");

                Console.WriteLine("Press enter to go back to the main menu. ");                
                Console.ReadKey();
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("Incorrect input, retrying to set grade using stored procedure.");

                // Create connection string 
                string connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=NewAgeHighSchool;Integrated Security=True";
    
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    using (SqlCommand command = new SqlCommand("Set_Grade", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        Console.WriteLine("Enter student ID: ");
                        int studentID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter student grade: ");
                        int studentGrade = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter subject name: ");
                        string subjectName = Console.ReadLine();


                        // Adding vales 
                        command.Parameters.AddWithValue("@StudentID", studentID);
                        command.Parameters.AddWithValue("@Grade", studentGrade);
                        command.Parameters.AddWithValue("@Subject", subjectName);

                        command.ExecuteNonQuery();

                        Console.WriteLine("Grade registered successfully.");
                    }
                }
            }
        }
    }
}
   



    



    
     

   


