using Chat.Data;
using Chat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.TestClient
{
    class ChatConsole
    {
        static void Main()
        {
            //Create and initialize the database

            Database.SetInitializer(new MigrateDatabaseToLatestVersion
                <ChatContext, Chat.Data.Migrations.Configuration>());

            var context = new ChatContext();
            using (context)
            {
                var user = new User { Username = "яверттъъуу", Password = "явертъъъъъъ", Picture = "....." };
                context.Users.Add(user);
                var chatRoom = new ChatRoom { Name = "chatroom", Users = new User[] { user } };
                context.ChatRooms.Add(chatRoom);
                var post = new Post { Date = DateTime.Now, UserId = user.Id, ChatRoomId = chatRoom.Id, Content = "ok" };
                context.Posts.Add(post);
                context.SaveChanges();
            }
        }
    }
}
