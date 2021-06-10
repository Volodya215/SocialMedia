using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    /// <summary>
    /// An interface for the UnitOfWork class, which provides access to repositories through separate properties and defines a general context for the repositories 
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// A property that contains a link to a repository with user profiles
        /// </summary>
        IUserProfileRepository UserProfileRepository { get; }

        /// <summary>
        /// A property that contains a link to a repository with posts 
        /// </summary>
        IPostRepository PostRepository { get; }

        /// <summary>
        /// A property that contains a link to the message repository 
        /// </summary>
        IMessageRepository MessageRepository { get; }

        /// <summary>
        /// Property that contains a link to the chat repository 
        /// </summary>
        IChatRepository ChatRepository { get; }

        /// <summary>
        /// A property that contains a link to the repository to work with subscriber data 
        /// </summary>
        IBloggerSubscriberRepository BloggerSubscriberRepository { get; }

        /// <summary>
        /// Save all changes to the database  
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database</returns>
        Task<int> SaveAsync();
    }
}
