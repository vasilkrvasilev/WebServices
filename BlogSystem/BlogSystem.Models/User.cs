using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30), MinLength(6)]
        public string Username { get; set; }

        [Required]
        [MaxLength(30), MinLength(6)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(40), MinLength(40)]
        public string AuthCode { get; set; }

        [MaxLength(50), MinLength(50)]
        public string SessionKey { get; set; }

        //public virtual ICollection<Comment> Threads { get; set; }
        //public virtual ICollection<Post> Posts { get; set; }

        //public User()
        //{
        //    this.Threads = new HashSet<Thread>();
        //    this.Posts = new HashSet<Post>();
        //    this.Votes = new HashSet<Vote>();
        //    this.Comments = new HashSet<Comment>();
        //}
    }
}
