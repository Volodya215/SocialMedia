using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_DAL.Entities
{
    public class BloggerSubscriber : BaseEntity
    {
        #nullable enable
        public string? BloggerId { get; set; }
        public string? SubscriberId { get; set; }

        #nullable disable
        public User Blogger { get; set; }
        public User Subscriber { get; set; }
    }
}
