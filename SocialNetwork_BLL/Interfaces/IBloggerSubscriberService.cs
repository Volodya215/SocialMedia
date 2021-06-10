using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IBloggerSubscriberService
    {
        /// <summary>
        ///  Asynchronously adds an element to the database
        /// </summary>
        /// <param name="model">Models type of BloggerSubscriberModel</param>
        /// <returns>Task</returns>
        Task AddAsync(BloggerSubscriberModel model);

        /// <summary>
        /// Deletes an item in the database for a given ID
        /// </summary>
        /// <param name="bloggerUserName">Blogger username</param>
        /// <param name="subscriberUserName">Subscriber username</param>
        /// <returns>Task</returns>
        Task DeleteAsync(string bloggerUserName, string subscriberUserName);

        /// <summary>
        /// Finds all subscribers to the user with this username
        /// </summary>
        /// <param name="userName">Username of user</param>
        /// <returns>List of all subscribers </returns>
        IEnumerable<string> GetAllFollowersByUserName(string userName);

        /// <summary>
        /// Finds everyone to whom the user is subscribed 
        /// </summary>
        /// <param name="userName">Username of user</param>
        /// <returns>List of everyone to whom the user is subscribed</returns>
        IEnumerable<string> GetAllFollowingByUserName(string userName);

        /// <summary>
        /// Checks if a user has subscribed to another user on theirs usernames 
        /// </summary>
        /// <param name="bloggerUserName">The one to whom it is signed </param>
        /// <param name="subscriberUserName">Subscriber username</param>
        /// <returns>True, if friends are false otherwise</returns>
        bool IsFriend(string bloggerUserName, string subscriberUserName);
    }
}
