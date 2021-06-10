using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    /// <summary>
    /// Contains general data about message
    /// </summary>
    public class MessageModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// The time of writing the message 
        /// </summary>
        public DateTime MessageTime { get; set; }

    }
}
