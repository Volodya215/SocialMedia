using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    /// <summary>
    /// Contains full information about message
    /// </summary>
    public class MessageAdminModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string ChatName { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// The time of writing the message 
        /// </summary>
        public DateTime MessageTime { get; set; }
    }
}
