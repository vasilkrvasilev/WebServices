using School.Data;
using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Client
{
    class SchoolConsole
    {
        public static void Main()
        {
            //Create and initialize the database

            Database.SetInitializer(new MigrateDatabaseToLatestVersion
                <SchoolContext, School.Data.Migrations.Configuration>());

            var context = new SchoolContext();
            using (context)
            {
                var school = new TownSchool { Name = "School", Location = "Town" };
                var firstStudent = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 9, Grade = 4, TownSchool = school };
                var secondStudent = new Student { FirstName = "Peter", LastName = "Petrov", Age = 15, Grade = 9, TownSchool = school };
                var firstMark = new Mark { Subject = "Math", Value = 5, Student = firstStudent };
                var secondMark = new Mark { Subject = "History", Value = 4, Student = secondStudent };
                var thirdMark = new Mark { Subject = "IT", Value = 6, Student = secondStudent };

                context.Marks.Add(firstMark);
                context.Marks.Add(secondMark);
                context.Marks.Add(thirdMark);
                context.Students.Add(firstStudent);
                context.Students.Add(secondStudent);
                context.TownSchools.Add(school);
                context.SaveChanges();
            }
        }
    }
}
