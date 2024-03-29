﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewAgeHS.Models;

public partial class Staff
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Title { get; set; } = null!;
    public DateTime EmploymentDate { get; set; }
    public decimal Salary { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public void NewEmployee() // Method for registering new staff
    {
        Console.WriteLine("Fill employee first name:");
        string FirstName = Console.ReadLine();
        Console.WriteLine("Fill employee last name:");
        string LastName = Console.ReadLine();
        Console.WriteLine("Fill employee title:");
        string Title = Console.ReadLine();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();
        Staff newEmployee = new Staff();
        {
            newEmployee.FirstName = FirstName;
            newEmployee.LastName = LastName;
            newEmployee.Title = Title;
        };
        Console.WriteLine($"New employee is registered. ");

        context.Staff.Add(newEmployee);
        context.SaveChanges();
    }
    public void ShowAllStaff() // Method for displaying all staff 
    {
        Console.Clear();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();
        var allStaff = from staff in context.Staff
                       select new
                       {
                           EmpId = staff.EmployeeId,
                           FirstName = staff.FirstName,
                           LastName = staff.LastName,
                           Title = staff.Title,
                           EmploymentDate = DateTime.Now.Year - ((DateTime)staff.EmploymentDate).Year,
                       };
        var table = new Table();

        table.AddColumn("EmpID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Title");
        table.AddColumn("Years in service");

        foreach (var item in allStaff)
        {
            table.AddRow(item.EmpId.ToString(),
                item.FirstName,
                item.LastName,
                item.Title,
                item.EmploymentDate.ToString());
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to main menu. ");
        Console.ReadKey();
    }
    public void ShowSpecificStaff() // Method for displaying specific staff
    {
        Console.Clear();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();

        Console.WriteLine("Choose:\n1. Principal\n2. Teachers\n3. Administrator");
        int userInput = int.Parse(Console.ReadLine());
        userInput = userInput - 1;
        string[] allTitles = { "Principal", "Teacher", "Administrator" };

        var specificStaff = context.Staff
            .Where(c => c.Title == allTitles[userInput]);

        var table = new Table();

        table.AddColumn("EmpID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Title");

        foreach (var item in specificStaff)
        {
            table.AddRow(item.EmployeeId.ToString(), item.FirstName, item.LastName, item.Title);
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to main menu. ");
        Console.ReadKey();
    }
    public void ShowAverageStaffSalary() // Method for displaying salaries 
    {
        Console.Clear();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();

       var staffSalaries = from staff in context.Staff
            join course in context.Courses on staff.EmployeeId equals course.FkemployeeId
            select new
            {
                CourseName = course.CourseName,
                Salary = staff.Salary,
            };

        // Group by course and calculate average salary
        var averageSalaries = staffSalaries
            .GroupBy(Course => new { Course.CourseName})
            .Select(group => new
            {
                CourseName = group.Key.CourseName,
                AverageSalary = group.Average(staff => staff.Salary),               
            });

        var table = new Table();

        table.AddColumn("Course");
        table.AddColumn("Average Salary by dept");
        

        foreach (var item in averageSalaries)
        {
            table.AddRow(item.CourseName, item.AverageSalary.ToString("f2"));
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to main menu. ");
        Console.ReadKey();
    }
    public void ShowSalaryByTitle() // Method for displaying salaries by title
    {
        Console.Clear();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();

        var staffSalaries = from staff in context.Staff
                            join course in context.Courses on staff.EmployeeId equals course.FkemployeeId
                            select new
                            {
                                Title = staff.Title,
                                Salary = staff.Salary,
                            };

        // Group by course and calculate average salary
        var totalSalaries = staffSalaries
            .GroupBy(Staff => new { Staff.Title})
            .Select(group => new
            {
                Title = group.Key.Title,
                TotalSalary = group.Sum(staff => staff.Salary),
            });

        var table = new Table();

        table.AddColumn("Title");
        table.AddColumn("Total Salary by profession");

        foreach (var item in totalSalaries)
        {
            table.AddRow(item.Title, item.TotalSalary.ToString("f2"));
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to main menu. ");
        Console.ReadKey();
    }
}




