namespace Chat.Data.Migrations
{
    using Chat.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ChatContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ChatContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //using (context)
            //{
            //    var user = new User { Username = "username", Password = "password", Picture = "....." };
            //    context.Users.Add(user);
            //    var chatRoom = new ChatRoom { Name = "chatroom", Users = new User[] { user } };
            //    context.ChatRooms.Add(chatRoom);
            //    var post = new Post { Date = DateTime.Now, UserId = user.Id, ChatRoomId = chatRoom.Id, Content = "ok" };
            //    context.Posts.Add(post);
            //    context.SaveChanges();
            //}

        }
    }
}
