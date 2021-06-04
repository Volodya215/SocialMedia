using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    public class ChatModel
    {
        public int Id { get; set; }
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }


        public ICollection<int> MessagesIds { get; set; }
    }
}
