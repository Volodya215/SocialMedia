using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    public class Chat : BaseEntity
    {
        [ForeignKey("FirstUser")]
        public string FirstUserId { get; set; }
        [ForeignKey("SecondUser")]
        public string SecondUserId { get; set; }
        public DateTime LastModify { get; set; }
        [NotMapped]
        public string Name { get; set; }

        public virtual User FirstUser { get; set; }
        public virtual User SecondUser { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
