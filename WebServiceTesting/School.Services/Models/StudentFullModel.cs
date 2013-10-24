using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Services.Models
{
    public class StudentFullModel
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int Grade { get; set; }

        public int MarksCount { get; set; }

        public IEnumerable<MarkModel> Marks { get; set; }

        public TownSchoolModel TownSchool { get; set; }

        public static StudentFullModel Convert(Student student)
        {
            StudentFullModel model = new StudentFullModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Grade = student.Grade,
                MarksCount = student.Marks.Count,
                Marks = (
                from mark in student.Marks
                select MarkModel.Convert(mark)).ToList(),
                TownSchool = new TownSchoolModel
                {
                    TownSchoolId = student.TownSchool.TownSchoolId,
                    Name = student.TownSchool.Name,
                    Location = student.TownSchool.Location,
                    StudentsCount = student.TownSchool.Students.Count
                }
            };

            return model;
        }
    }
}