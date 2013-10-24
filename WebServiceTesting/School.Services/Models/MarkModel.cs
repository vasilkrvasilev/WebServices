using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Services.Models
{
    public class MarkModel
    {
        public int MarkId { get; set; }

        public string Subject { get; set; }

        public int Value { get; set; }

        public int StudentId { get; set; }

        public static MarkModel Convert(Mark mark)
        {
            MarkModel model = new MarkModel
            {
                MarkId = mark.MarkId,
                Subject = mark.Subject,
                Value = mark.Value,
                StudentId = mark.StudentId
            };

            return model;
        }
    }
}