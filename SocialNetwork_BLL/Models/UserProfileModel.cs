using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string City { get; set; } = "No information";
        public string Work { get; set; } = "No information";
        public string Hobby { get; set; } = "No information";
        public string About { get; set; } = "No information";
    }
}
