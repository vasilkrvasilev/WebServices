using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember(IsRequired = true)]
        public string Password { get; set; }

        [DataMember(IsRequired = true)]
        public string Username { get; set; }

        public static UserModel Convert(User user)
        {
            UserModel model = new UserModel
            {
                Username = user.Username,
                Password = user.Password
            };

            return model;
        }
    }
}