using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Services.Models
{
    public class TownSchoolModel
    {
        public int TownSchoolId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int StudentsCount { get; set; }

        public static TownSchoolModel Convert(TownSchool school)
        {
            TownSchoolModel model = new TownSchoolModel
            {
                TownSchoolId = school.TownSchoolId,
                Name = school.Name,
                Location = school.Location,
                StudentsCount = school.Students.Count
            };

            return model;
        }
    }
}