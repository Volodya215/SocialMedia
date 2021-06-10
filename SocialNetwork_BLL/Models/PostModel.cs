using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    /// <summary>
    /// Contains data about post
    /// </summary>
    public class PostModel
    {
        public int Id { get; set; }
        /// <summary>
        /// The topic of the post 
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// Posting time 
        /// </summary>
        public DateTime DateOfPost { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
