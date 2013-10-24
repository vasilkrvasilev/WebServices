using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    [DataContract]
    public class UserFullModel : UserModel
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember(IsRequired = true)]
        public string Picture { get; set; }

        [DataMember]
        public bool IsOnline { get; set; }

        [DataMember]
        public int PostCount { get; set; }

        [DataMember]
        public int ChatRoomCount { get; set; }

        public static UserFullModel Convert(User user)
        {
            UserFullModel model = new UserFullModel
            {
                UserId = user.Id,
                Picture = user.Picture,
                Username = user.Username,
                IsOnline =user.IsOnline,
                PostCount = user.Posts.Count,
                ChatRoomCount = user.ChatRooms.Count
            };

            return model;
        }
    }
}