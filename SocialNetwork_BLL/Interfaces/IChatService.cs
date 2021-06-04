using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IChatService : ICrud<ChatModel>
    {
        IEnumerable<ChatModel> GetChatsWithUserByUserId(string userId);
        IEnumerable<MessageModel> GetAllMessagesFromChatByChatId(int chatId);
    }
}
