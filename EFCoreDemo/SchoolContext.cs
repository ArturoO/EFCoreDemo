﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreDemo
{
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
