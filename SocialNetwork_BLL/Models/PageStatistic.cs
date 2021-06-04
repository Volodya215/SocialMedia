using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Models
{
    public class PageStatistic
    {
        public int PostsCount { get; set; } = 0;
        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
    }
}
