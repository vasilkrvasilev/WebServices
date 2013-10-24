using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    [DataContract]
    public class ChatRoomModel
    {
        [DataMember]
        public int ChatRoomId { get; set; }

        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember]
        public int PostCount { get; set; }

        [DataMember]
        public int UserCount { get; set; }

        public static ChatRoomModel Convert(ChatRoom chatRoom)
        {
            ChatRoomModel model = new ChatRoomModel
            {
                ChatRoomId = chatRoom.Id,
                Name = chatRoom.Name,
                PostCount = chatRoom.Posts.Count,
                UserCount = chatRoom.Users.Count
            };

            return model;
        }
    }
}