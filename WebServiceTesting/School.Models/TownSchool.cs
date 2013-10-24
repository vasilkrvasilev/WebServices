using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Models
{
    public class TownSchool
    {
        private ICollection<Student> students;

        public TownSchool()
        {
            this.students = new HashSet<Student>();
        }

        [Key]
        public int TownSchoolId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Location { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }
    }
}
