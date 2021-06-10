using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IBloggerSubscriberRepository : IRepository<BloggerSubscriber>
    {
        /// <summary>
        /// Finds all data about blogger and subscribers with detailed information about them 
        /// </summary>
        /// <returns>Returns a query string with an expression tree </returns>
        IQueryable<BloggerSubscriber> FindAllWithDetails();

        /// <summary>
        /// Determines whether the user is subscribed to the blogger by their usernames 
        /// </summary>
        /// <param name="bloggerUserName">Blogger username</param>
        /// <param name="subscriberUserName">Subscriber username</param>
        /// <returns>True if the subscriber is subscribed to the blogger, otherwise false </returns>
        bool IsFriends(string bloggerUserName, string subscriberUserName);
    }
}
