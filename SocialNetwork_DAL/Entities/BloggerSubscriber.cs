using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    public class BloggerSubscriber : BaseEntity
    {
        public string BloggerId { get; set; }
        public User Blogger { get; set; }

        public string SubscriberId { get; set; }
        public User Subscriber { get; set; }
    }
}
