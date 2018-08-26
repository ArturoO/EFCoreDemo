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
            var course1 = new Course() {
                CourseName = "Cooking",
            };
            var course2 = new Course() {
                CourseName = "Sewing",
            };
            var student1 = new Student() {
                Name = "Adam",
                Gender = "male",
                Age = 17,
                Course = course1,
            };
            var student2 = new Student()
            {
                Name = "Monica",
                Gender = "female",
                Age = 18,
                Course = course1,
            };
            var student3 = new Student()
            {
                Name = "John",
                Gender = "male",
                Age = 16,
                Course = course2,
            };
            var student4 = new Student()
            {
                Name = "Alice",
                Gender = "female",
                Age = 19,
                Course = course2,
            };
            var student5 = new Student()
            {
                Name = "Ron",
                Gender = "male",
                Age = 20,
                Course = course1,
            };
            using (var context = new SchoolContext())
            {
                context.Courses.Add(course1);
                context.Courses.Add(course2);
                context.Students.Add(student1);
                context.Students.Add(student2);
                context.Students.Add(student3);
                context.Students.Add(student4);
                context.Students.Add(student5);
                context.SaveChanges();
            }


            return;
            UserInterface();
        }

        public static void UserInterface()
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

            while (true)
            {
                var cmd = Console.ReadLine();
                if (cmd == "exit")
                    break;
                switch (cmd)
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
