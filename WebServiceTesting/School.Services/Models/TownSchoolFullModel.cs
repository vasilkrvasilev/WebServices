using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Services.Models
{
    public class TownSchoolFullModel
    {
        public int TownSchoolId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int StudentsCount { get; set; }

        public IEnumerable<StudentModel> Students { get; set; }

        public static TownSchoolFullModel Convert(TownSchool school)
        {
            TownSchoolFullModel model = new TownSchoolFullModel
            {
                TownSchoolId = school.TownSchoolId,
                Name = school.Name,
                Location = school.Location,
                StudentsCount = school.Students.Count,
                Students = (
                from student in school.Students
                select StudentModel.Convert(student)).ToList()
            };

            return model;
        }
    }
}