using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Spectre.Console;
using Microsoft.Identity.Client;

namespace NewAgeHS.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int FkemployeeId { get; set; }

    public virtual Staff Fkemp { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    
    public void ShowAllCourses()
    {
        Console.Clear();
        NewAgeHighSchoolContext dbcontext = new NewAgeHighSchoolContext();
        var courses = dbcontext.Courses;

        var table = new Table();

        table.AddColumn("Course name");
        foreach (var course in courses)
        {
            table.AddRow(course.CourseName);            
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to main menu. ");
        Console.ReadKey();
    }
    public void ShowCourseDetails()
    {
        Console.Clear();
        NewAgeHighSchoolContext dbcontext = new NewAgeHighSchoolContext();        
        Console.WriteLine("Choose:\n1. Programmering\n2. Ekonomi\n3. Datanätverk");
        int userInput = int.Parse(Console.ReadLine());
        userInput = userInput - 1;
        string [] courseName = { "Programmering", "Ekonomi", "Datanätverk"}; 
        var courseDetails = dbcontext.Students.Include(c => c.Fkcourse).ToList()
            .Where(c => c.Fkcourse.CourseName == courseName[userInput]);
        var table = new Table();

        table.AddColumn("Student first name");
        table.AddColumn("Student last name");
        table.AddColumn("Course name");
        foreach (var i in courseDetails)
        {
            table.AddRow( i.FirstName, i.LastName, i.Fkcourse.CourseName);
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to main menu. ");
        Console.ReadKey();
    }
}
