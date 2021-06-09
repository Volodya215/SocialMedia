using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IChatService : ICrud<ChatModel>
    {
        /// <summary>
        /// Returns a list of all chats with this user by his ID 
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of ChatModel</returns>
        IEnumerable<ChatModel> GetChatsWithUserByUserId(string userId);

        /// <summary>
        /// Returns all messages from the chat by its id
        /// </summary>
        /// <param name="chatId">Chat id</param>
        /// <returns>All messages from the chat by its id</returns>
        IEnumerable<MessageModel> GetAllMessagesFromChatByChatId(int chatId);

        /// <summary>
        /// Returns chat IDs by specified usernames. Creates a chat if it does not exist 
        /// </summary>
        /// <param name="firstUser">The first chat user </param>
        /// <param name="secondUser">The second chat user </param>
        /// <returns>Returns the chat id </returns>
        Task<int> GetChatIdByUsernames(string firstUser, string secondUser);
    }
}
