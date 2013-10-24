using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    [DataContract]
    public class PostFullModel : PostModel
    {
        [DataMember]
        public UserModel User { get; set; }

        [DataMember]
        public ChatRoomModel ChatRoom { get; set; }

        public static PostFullModel Convert(Post post)
        {
            PostFullModel model = new PostFullModel
            {
                PostId = post.Id,
                Date = post.Date,
                Content = post.Content,
                IsFile = post.IsFile,
                User = new UserFullModel
                {
                    UserId = post.User.Id,
                    Picture = post.User.Picture,
                    Username = post.User.Username,
                    IsOnline =post.User.IsOnline,
                    PostCount = post.User.Posts.Count,
                    ChatRoomCount = post.User.ChatRooms.Count
                },
                ChatRoom = new ChatRoomModel
                {
                    ChatRoomId = post.ChatRoom.Id,
                    Name = post.ChatRoom.Name,
                    PostCount = post.ChatRoom.Posts.Count,
                    UserCount = post.ChatRoom.Users.Count
                }
            };

            return model;
        }
    }
}