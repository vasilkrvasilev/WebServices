using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Models;

namespace Chat.Data
{
    public class ChatContext : DbContext
    {
        public ChatContext()
            : base("ChatDb")
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ChatRoom> ChatRooms { get; set; }
    }
}
