﻿namespace SocialNetwork_DAL.Entities
{
    /// <summary>
    /// Contains data about blogger and his subscriber
    /// </summary>
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
