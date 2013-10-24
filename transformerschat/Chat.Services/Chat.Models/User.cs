using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class User
    {
        private ICollection<Post> posts;
        private ICollection<ChatRoom> chatRooms;

        public User()
        {
            this.posts = new HashSet<Post>();
            this.chatRooms = new HashSet<ChatRoom>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(26)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(26)]
        public string Password { get; set; }

        public bool IsOnline { get; set; }

        public virtual ICollection<ChatRoom> ChatRooms
        {
            get { return this.chatRooms; }
            set { this.chatRooms = value; }
        }

        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }
 
    }
}