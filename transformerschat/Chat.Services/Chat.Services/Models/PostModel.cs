using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    [DataContract]
    public class PostModel
    {
        [DataMember] 
        public int PostId { get; set; }

        [DataMember]//(IsRequired = true)]
        public DateTime Date { get; set; }

        [DataMember]//(IsRequired = true)]
        public string Content { get; set; }

        public bool IsFile { get; set; }

        [DataMember]//(IsRequired = true)]
        public int UserId { get; set; }

        [DataMember]//(IsRequired = true)]
        public int ChatRoomId { get; set; }

        public static PostModel Convert(Post post)
        {
            PostModel model = new PostModel
            {
                PostId = post.Id,
                Date = post.Date,
                Content = post.Content,
                IsFile = post.IsFile,
                UserId = post.UserId,
                ChatRoomId = post.ChatRoomId
            };

            return model;
        }
    }
}