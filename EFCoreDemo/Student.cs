using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDemo
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }
    }
}
