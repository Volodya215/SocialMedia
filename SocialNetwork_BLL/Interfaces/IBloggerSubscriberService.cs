using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IBloggerSubscriberService
    {
        Task AddAsync(BloggerSubscriberModel model);

        Task DeleteAsync(BloggerSubscriberModel model);

        IEnumerable<string> GetAllFollowersByUserName(string userName);
        IEnumerable<string> GetAllFollowingByUserName(string userName);

        bool IsFriend(string bloggerUserName, string subscriberUserName);
    }
}
