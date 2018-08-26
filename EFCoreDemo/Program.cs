using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> commands = new List<string>() {
                "exit",
                "students-show",
                "student-add",
                "student-delete",
                "student-edit",
            };
            
            Console.WriteLine("Welcome to EFCoreDemo application, please type your command.");
            Console.WriteLine("Available commands: " + string.Join(',', commands.ToArray()));

            while(true)
            {
                var cmd = Console.ReadLine();
                if (cmd == "exit")
                    break;
                switch(cmd)
                {
                    case "students-show":
                        ShowStudents();
                        break;
                    case "student-add":
                        AddStudent();
                        break;
                    case "student-delete":
                        DeleteStudent();
                        break;
                    case "student-edit":
                        EditStudent();
                        break;
                    default:
                        Console.WriteLine("Incorrect command, please try again");
                        break;
                }
            }
            
            Console.WriteLine("Goodbye.");
            
        }

        public static void ShowStudents()
        {
            using (var context = new SchoolContext())
            {
                var students = context.Students.ToList();
                foreach (var student in students)
                    Console.WriteLine($"{student.StudentId}: {student.Name}");
            }
        }

        public static void AddStudent()
        {
            Console.WriteLine("Please provide student name");
            var studentName = Console.ReadLine();
            if(studentName=="")
            {
                Console.WriteLine("Error");
                return;
            }
            using (var context = new SchoolContext())
            {
                var std = new Student()
                {
                    Name = studentName
                };

                context.Students.Add(std);
                context.SaveChanges();
            }
            Console.WriteLine("Student added.");
        }

        public static void DeleteStudent()
        {
            Console.WriteLine("Please provide student id");
            var studentId = int.Parse(Console.ReadLine());
            if (studentId<=0)
            {
                Console.WriteLine("Error");
                return;
            }
            using (var context = new SchoolContext())
            {
                var student = context.Students.First(x => x.StudentId == studentId);
                context.Students.Remove(student);
                context.SaveChanges();
            }
            Console.WriteLine("Student removed.");
        }

        public static void EditStudent()
        {
            Console.WriteLine("Please provide student id");
            var studentId = int.Parse(Console.ReadLine());
            if (studentId <= 0)
            {
                Console.WriteLine("Error");
                return;
            }
            Console.WriteLine("Please provide student name");
            var studentName = Console.ReadLine();
            if (studentName == "")
            {
                Console.WriteLine("Error");
                return;
            }
            using (var context = new SchoolContext())
            {
                var student = context.Students.First(x => x.StudentId == studentId);
                student.Name = studentName;
                context.Students.Update(student);
                context.SaveChanges();
            }
            Console.WriteLine("Student updated.");
        }

    }
}
