using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        public static void AddStudent()
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

    }
}
