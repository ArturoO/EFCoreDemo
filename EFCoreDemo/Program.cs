using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowStudents();
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
            using (var context = new SchoolContext())
            {

                var std = new Student()
                {
                    Name = "Jolene"
                };

                context.Students.Add(std);
                context.SaveChanges();
            }
        }

    }
}
