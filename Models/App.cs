using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Spectre.Console;

namespace NewAgeHS.Models
{
    public class App
    {
        public void RunApp()
        { 
            Console.Clear();
            Console.WriteLine("Welcome to New Age High School. \n\nPress Enter to see start menu.");
            Console.ReadKey();
            while (true)
            {
                try
                {
                    Console.Clear();    
                    Console.WriteLine("Press: \n1. Staff \n2. Students\n3. Courses\n4. Reports and Grades\n0 to exit program");
                    int userChoice = int.Parse(Console.ReadLine());

                    if (userChoice == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Choose from the menu below:");
                        Console.WriteLine("Press: \n1. All Staff \n2. Staff with specific title\n3. To register new staff\n0. Previous page");
                        int staffChoice = int.Parse(Console.ReadLine());
                        if (staffChoice == 1)
                        {
                            Staff allStaffInfo = new Staff();
                            allStaffInfo.ShowAllStaff();
                        }
                        else if (staffChoice == 2)
                        {
                            Staff staffInfo = new Staff();
                            staffInfo.ShowSpecificStaff();
                        }
                        else if (staffChoice == 3)
                        {
                            Staff newEmp = new Staff();
                            newEmp.NewEmployee();
                        }
                    }
                    else if (userChoice == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Press:\n1. All Students' information\n2. Sorted students' information\n3. Students by class name\n4. Register a new student\n0. Previous page");
                        int studentChoice = int.Parse(Console.ReadLine());
                        if (studentChoice == 1)
                        {
                            Student studentInformation = new Student();
                            studentInformation.ShowStudents();
                        }
                        else if (studentChoice == 2)
                        {
                            Student sortedStudentInformation = new Student();
                            sortedStudentInformation.ShowStudentsSorted();
                        }
                        else if (studentChoice == 3)
                        {
                            Student studentInClass = new Student();
                            studentInClass.ShowClassStudents();
                        }
                        else if (studentChoice == 4)
                        {
                            Student addNewStudent = new Student();
                            addNewStudent.NewStudent();
                        }
                    }
                    else if (userChoice == 3)
                    {
                        Console.Clear();
                        Console.WriteLine("Press:\n1. All courses\n2. Courses with participants\n0. Previous page");
                        int courseChoice = int.Parse(Console.ReadLine());
                        if (courseChoice == 1)
                        {
                            Course showCourses = new Course();
                            showCourses.ShowAllCourses();
                        }
                        else if (courseChoice == 2)
                        {
                            Course studentsInCourse = new Course();
                            studentsInCourse.ShowCourseDetails();
                        }
                    }
                    else if (userChoice == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("Press:\n1. Student average grade\n2. Average grades by course\n3. All students' grades\n4. Last month grades\n0. Previous page ");
                        int courseChoice = int.Parse(Console.ReadLine());
                        if (courseChoice == 1)
                        {
                            ReportCard studentAverageGrade = new ReportCard(); 
                            studentAverageGrade.StudentAverageGrade();
                        }
                        else if (courseChoice == 2)
                        {
                            ReportCard getAverageCourseGrade = new ReportCard();
                            getAverageCourseGrade.CourseAverageGrades();
                        }
                        else if (courseChoice == 3)
                        {
                            ReportCard studentAllGrades = new ReportCard();
                            studentAllGrades.StudentReportCard();
                        }
                        else if(courseChoice == 4)
                        {
                            ReportCard lastMonthGrades = new ReportCard();
                            lastMonthGrades.LastMonthGrades();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You are exiting the program, press Enter to confirm:");
                        Console.ReadKey();
                        Console.WriteLine("Good Bye!");
                        break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Opps! Invalid input.\nPress Enter to return to main menu:");
                    Console.ReadKey();                    
                }
            }
        }
    } 
}
   