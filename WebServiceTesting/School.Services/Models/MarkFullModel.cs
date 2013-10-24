using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Services.Models
{
    public class MarkFullModel
    {
        public int MarkId { get; set; }

        public string Subject { get; set; }

        public int Value { get; set; }

        public StudentModel Student { get; set; }

        public static MarkFullModel Convert(Mark mark)
        {
            MarkFullModel model = new MarkFullModel
            {
                MarkId = mark.MarkId,
                Subject = mark.Subject,
                Value = mark.Value,
                Student = new StudentModel
                {
                    StudentId = mark.Student.StudentId,
                    FirstName = mark.Student.FirstName,
                    LastName = mark.Student.LastName,
                    Age = mark.Student.Age,
                    Grade = mark.Student.Grade,
                    MarksCount = mark.Student.Marks.Count,
                    TownSchoolId = mark.Student.TownSchoolId
                }
            };

            return model;
        }
    }
}