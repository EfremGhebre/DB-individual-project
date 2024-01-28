using Spectre.Console;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using System.Data;


namespace NewAgeHS.Models;

public partial class Student
{
    public int StudId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? FkclassName { get; set; }

    public int FkcourseId { get; set; }

    public virtual Class? FkclassNameNavigation { get; set; }

    public virtual Course Fkcourse { get; set; } = null!;

    public virtual ICollection<ReportCard> ReportCards { get; set; } = new List<ReportCard>();

    public void ShowStudents() // Method that displays all students information
    {
        Console.Clear();
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();
        var allStudents = from student in context.Students                    
                    select new
                    {
                        StudID = student.StudId,
                        FirstName = student.FirstName,
                        LastName = student.LastName,                        
                        FkclassName = student.FkclassName                        
                    };
        var table = new Table(); // Creates a table and displays chosen details 

        table.AddColumn("StudID");
        table.AddColumn("First name");
        table.AddColumn("Last name");
        table.AddColumn("FKClassName");      

        foreach (var i in allStudents)
        {
            table.AddRow(i.StudID.ToString(), i.FirstName, i.LastName, i.FkclassName);
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Press enter to go back to main menu. ");
        Console.ReadKey();

    }
    public void ShowStudentsSorted() // Method that displays students' information sorted by users' choice
    {
        Console.Clear();
        using (var context = new NewAgeHighSchoolContext())
        {
            Console.WriteLine("Choose sorting method:\nPress ");
            Console.WriteLine("1. Sort by first name ascending");
            Console.WriteLine("2. Sort by first name descending");
            Console.WriteLine("3. Sort by last name ascending");
            Console.WriteLine("4. Sort by last name descending");

            int choice = int.Parse(Console.ReadLine());

            var students = GetSortedStudents(context, choice);

            var table = new Table();

            table.AddColumn("StudID");
            table.AddColumn("FirstName");
            table.AddColumn("LastName");

            foreach (var student in students)
            {
                table.AddRow(student.StudId.ToString(), student.FirstName, student.LastName);
            }
            AnsiConsole.Write(table);
            Console.WriteLine("Press enter to go back to main menu. ");
            Console.ReadKey();
        }
    }

    static IQueryable<Student> GetSortedStudents(NewAgeHighSchoolContext context, int choice) // Switch statement to sort students' information
    {
        switch (choice)
        {
            case 1:
                return context.Students.OrderBy(student => student.FirstName); 
            case 2:
                return context.Students.OrderByDescending(student => student.FirstName); 
            case 3:
                return context.Students.OrderBy(student => student.LastName); 
            case 4:
                return context.Students.OrderByDescending(student => student.LastName);
            default:
                throw new ArgumentException("Invalid choice.");
        }
    }
    public void NewStudent() // Method for registering new student 
    {
        Console.WriteLine("Fill student first name:");
        string FirstName = Console.ReadLine();
        Console.WriteLine("Fill student last name:");
        string LastName = Console.ReadLine();
        Console.WriteLine("Fill class name:");
        string FkclassName = Console.ReadLine();
        Console.WriteLine("Fill course ID:");
        int FkcourseId = int.Parse(Console.ReadLine());
        NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();
        Student newStudent = new Student();
        {   
            newStudent.FirstName = FirstName;
            newStudent.LastName = LastName; 
            newStudent.FkclassName = FkclassName;
            newStudent.FkcourseId = FkcourseId;
        };
        Console.WriteLine($"New student is registered. ");

        context.Students.Add(newStudent);
        context.SaveChanges();
    }
    public void ShowClassStudents() // Method that displays students in a specific class  
    {
            Console.Clear();
            NewAgeHighSchoolContext context = new NewAgeHighSchoolContext();

            Console.WriteLine("Choose:\n1. 9A\n2. 9B\n3. 10A");
            int userInput = int.Parse(Console.ReadLine());
            userInput = userInput - 1;
            string[] allClassNames = { "9A", "9B", "10A" };

            var specificStudentInClass = context.Students
                .Where(c => c.FkclassName == allClassNames[userInput]);

            var table = new Table();

            table.AddColumn("EmpID");
            table.AddColumn("FirstName");
            table.AddColumn("LastName");
            table.AddColumn("Title");

            foreach (var item in specificStudentInClass)
            {
                table.AddRow(item.StudId.ToString(), item.FirstName, item.LastName, item.FkclassName);
            }
            AnsiConsole.Write(table);
            Console.WriteLine("Press enter to go back to main menu. ");
            Console.ReadKey();
    }
    public void GetStudentInfo() // Get student information by student ID
    {
        // Create connection string 
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=NewAgeHighSchool;Integrated Security=True";

        // Get student ID from user input
        Console.Write("Enter Student ID: ");
        int studentID = Convert.ToInt32(Console.ReadLine());

        // Create a SqlConnection object
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Create a SqlCommand object with a parameterized SQL command

            using (SqlCommand command = new SqlCommand("ShowStudentInfo", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                // Add the StudentID parameter
                command.Parameters.AddWithValue("@InputStudentID", studentID);

                // Execute the command and read the results
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Display student information
                        Console.WriteLine($"StudentID: {reader["StudID"]}");
                        Console.WriteLine($"FirstName: {reader["FirstName"]}");
                        Console.WriteLine($"LastName: {reader["LastName"]}");
                        Console.WriteLine($"FkclassName: {reader["FkclassName"]}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
                Console.WriteLine("Press enter to go back to main menu. ");
                Console.ReadKey();
            }
        }
    }
}