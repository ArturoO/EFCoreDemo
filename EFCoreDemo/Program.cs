using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {

                var std = new Student()
                {
                    Name = "Bill"
                };

                context.Students.Add(std);
                context.SaveChanges();
            }
        }

        public class Student
        {
            public int StudentId { get; set; }
            public string Name { get; set; }
        }

        public class Course
        {
            public int CourseId { get; set; }
            public string CourseName { get; set; }
        }

        public class SchoolContext : DbContext
        {
            public DbSet<Student> Students { get; set; }
            public DbSet<Course> Courses { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(@"Data Source=ARTUROO-PC;Initial Catalog=SchoolDB;Integrated Security=True;Pooling=False");
                //Data Source=ARTUROO-PC;Initial Catalog=SchoolDB;Integrated Security=True;Pooling=False
            }
        }

    }
}
