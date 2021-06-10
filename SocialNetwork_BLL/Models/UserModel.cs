using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    /// <summary>
    /// Conatins data about user
    /// </summary>
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        /// <summary>
        /// Role in system
        /// </summary>
        public string Role { get; set; } = "Customer";
        public int UserProfileId { get; set; }
        public ICollection<int> PostsIds { get; set; }
    }
}