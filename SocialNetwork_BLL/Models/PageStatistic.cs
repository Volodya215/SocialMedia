using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    /// <summary>
    /// Contains data about page such as count of posts, followers and following
    /// </summary>
    public class PageStatistic
    {
        public int PostsCount { get; set; } = 0;
        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
    }
}
