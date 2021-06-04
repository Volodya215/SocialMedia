using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public DateTime DateOfPost { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
