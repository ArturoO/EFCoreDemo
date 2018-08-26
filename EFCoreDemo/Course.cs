using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDemo
{
    public class Course
    {
        ICollection<Student> students = new List<Student>();

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public ICollection<Student> Students { get { return students; } set { students = value; } }


    }
}
