using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int ChatRoomId { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string Content { get; set; }

        public bool IsFile { get; set; }
    }
}