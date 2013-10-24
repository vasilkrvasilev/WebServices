using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    [DataContract]
    public class ChatRoomFullModel : ChatRoomModel
    {
        [DataMember]
        public IEnumerable<PostModel> Posts { get; set; }

        [DataMember(IsRequired = true)]
        public IEnumerable<UserModel> Users { get; set; }

        public static ChatRoomFullModel Convert(ChatRoom chatRoom)
        {
            ChatRoomFullModel model = new ChatRoomFullModel
            {
                ChatRoomId = chatRoom.Id,
                Name = chatRoom.Name,
                PostCount = chatRoom.Posts.Count,
                UserCount = chatRoom.Users.Count,
                Posts = (
                from post in chatRoom.Posts
                select PostModel.Convert(post)).ToList(),
                Users = (
                from user in chatRoom.Users
                select UserModel.Convert(user)).ToList()
            };

            return model;
        }
    }
}