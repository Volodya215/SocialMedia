using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    /// <summary>
    /// Contains data about user chat
    /// </summary>
    public class Chat : BaseEntity
    {
        [ForeignKey("FirstUser")]
        public string FirstUserId { get; set; }
        [ForeignKey("SecondUser")]
        public string SecondUserId { get; set; }
        /// <summary>
        /// Time of last message in chat
        /// </summary>
        public DateTime LastModify { get; set; }
        /// <summary>
        /// Chat name for user
        /// </summary>
        [NotMapped]
        public string Name { get; set; }

        public virtual User FirstUser { get; set; }
        public virtual User SecondUser { get; set; }
        /// <summary>
        /// list of messages from chat
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }
    }
}
