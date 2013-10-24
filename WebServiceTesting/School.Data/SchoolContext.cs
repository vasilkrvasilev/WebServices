using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
            : base("Student")
        {
        }

        public DbSet<TownSchool> TownSchools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Mark> Marks { get; set; }
    }
}
