using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
        public DateTime MessageTime { get; set; }

    }
}
