using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IMessageService : ICrud<MessageModel>
    {
        /// <summary>
        /// Returns all messages written by a user with a specified ID 
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>List of messages</returns>
        IEnumerable<MessageModel> GetAllUserMessagesByUserId(string id);

        /// <summary>
        /// Returns all messages from database 
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>List of messages data</returns>
        IEnumerable<MessageAdminModel> GetAllMessagesForAdmin();
    }
}
