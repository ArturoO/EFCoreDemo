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
            return;
            UserInterface();
        }

        public static void CreateStudentAndCourseThenRemoveStudent()
        {
            using (var context = new SchoolContext())
            {
                //add new course and a student. 
                //assign student to course
                //save changes
                //var ChessCourse = new Course() {
                //    CourseName = "Chess",
                //};
                //var StudentAnthony = new Student()
                //{
                //    Name = "Anthony",
                //    Age = 22,
                //    Gender = "male",
                //};
                //ChessCourse.Students.Add(StudentAnthony);
                //context.Courses.Add(ChessCourse);
                //context.SaveChanges();

                //remove student from course
                //student will still exist in the db table
                var course = context.Courses
                    .Include(x => x.Students)
                    .Single(x => x.CourseName == "Chess");

                var studentAnthony = course.Students.Single(x => x.Name == "Anthony");
                course.Students.Remove(studentAnthony);
                context.SaveChanges();


                //var student = context.Students
                //    .Include(x => x.Course)
                //    .Single(x => x.Name == "Monica");
                //Console.WriteLine($"Student id: {student.StudentId} of name: {student.Name} is attending course: {student.Course.CourseName}");
            }
        }

        public static void FetchStudentAndItsCourse()
        {
            //return Monica student with information of her course
            using (var context = new SchoolContext())
            {
                var student = context.Students
                    .Include(x => x.Course)
                    .Single(x => x.Name == "Monica");
                Console.WriteLine($"Student id: {student.StudentId} of name: {student.Name} is attending course: {student.Course.CourseName}");
            }
        }

        public static void AddStudentToCourseAndThenRemoveHim()
        {
            using (var context = new SchoolContext())
            {
                var course = context.Courses
                    .Include(x => x.Students)
                    .Single(x => x.CourseName == "Cooking");
                Console.WriteLine($"Course id: {course.CourseId} and its name is: {course.CourseName}");

                Console.WriteLine("Students attending the course:");
                foreach (var student in course.Students)
                {
                    Console.WriteLine($"Student id: {student.StudentId}, name: {student.Name}, gender: {student.Gender}, age: {student.Age}");
                }


                Console.WriteLine("New student is attending course, updated list:");
                var studentRon = new Student()
                {
                    Name = "Ron",
                    Age = 17,
                    Gender = "male",
                };
                course.Students.Add(studentRon);
                context.SaveChanges();
                course = context.Courses
                    .Include(x => x.Students)
                    .Single(x => x.CourseName == "Cooking");
                foreach (var student in course.Students)
                {
                    Console.WriteLine($"Student id: {student.StudentId}, name: {student.Name}, gender: {student.Gender}, age: {student.Age}");
                }

                Console.WriteLine("Student has resigned, updated list:");
                course.Students.Remove(studentRon);
                context.SaveChanges();
                course = context.Courses
                    .Include(x => x.Students)
                    .Single(x => x.CourseName == "Cooking");
                foreach (var student in course.Students)
                {
                    Console.WriteLine($"Student id: {student.StudentId}, name: {student.Name}, gender: {student.Gender}, age: {student.Age}");
                }
            }
        }

        public static void FetchCourseAndItsStudents()
        {
            using (var context = new SchoolContext())
            {
                var course = context.Courses
                    .Include(x => x.Students)
                    .Single(x => x.CourseName == "Cooking");
                Console.WriteLine($"Course id: {course.CourseId} and its name is: {course.CourseName}");

                Console.WriteLine("Students attending the course:");
                foreach (var student in course.Students)
                {
                    Console.WriteLine($"Student id: {student.StudentId}, name: {student.Name}, gender: {student.Gender}, age: {student.Age}");
                }

            }
        }


        public static void AddSampleData()
        {
            var course1 = new Course()
            {
                CourseName = "Cooking",
            };
            var course2 = new Course()
            {
                CourseName = "Sewing",
            };
            var student1 = new Student()
            {
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
