using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Models
{
    public class Mark
    {
        [Key]
        public int MarkId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
