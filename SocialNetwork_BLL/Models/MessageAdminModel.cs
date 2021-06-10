using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    public class MessageAdminModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string ChatName { get; set; }
        public string Content { get; set; }
        public DateTime MessageTime { get; set; }
    }
}
