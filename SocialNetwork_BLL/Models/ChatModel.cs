using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    /// <summary>
    /// Contains data about user chat
    /// </summary>
    public class ChatModel
    {
        public int Id { get; set; }
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
        /// <summary>
        /// Time of last message in chat
        /// </summary>
        public DateTime LastModify { get; set; }
        /// <summary>
        /// Chat name
        /// </summary>
        public string Name { get; set; }


        public ICollection<int> MessagesIds { get; set; }
    }
}
