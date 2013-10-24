using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Services.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int Grade { get; set; }

        public int MarksCount { get; set; }

        public int TownSchoolId { get; set; }

        public static StudentModel Convert(Student student)
        {
            StudentModel model = new StudentModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Grade = student.Grade,
                MarksCount = student.Marks.Count,
                TownSchoolId = student.TownSchoolId
            };

            return model;
        }
    }
}