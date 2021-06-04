using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } = "Customer";
        //public UserProfileModel UserProfile { get; set; }
        public int UserProfileId { get; set; }
        public ICollection<int> PostsIds { get; set; }
    }
}
