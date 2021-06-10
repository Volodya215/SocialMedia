using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    /// <summary>
    /// Contains data about user profile
    /// </summary>
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string City { get; set; }
        public string Work { get; set; }
        public string Hobby { get; set; } 
        public string About { get; set; }
    }
}
