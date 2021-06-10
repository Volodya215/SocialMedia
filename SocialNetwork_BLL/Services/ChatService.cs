using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_BLL.Models;
using SocialNetwork_BLL.Validation;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Services
{
    /// <summary>
    /// Service for processing data related to chats
    /// </summary>
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Injection dependence in this service 
        /// </summary>
        /// <param name="iow">Class Unit of Work</param>
        /// <param name="mapper">Mapper for mapping data</param>
        /// <param name="userManager">To work with the date of the user in the database </param>
        public ChatService(IUnitOfWork iow, IMapper mapper, UserManager<User> userManager)
        {
            Database = iow;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IEnumerable<MessageModel> GetAllMessagesFromChatByChatId(int chatId)
        {
            if (chatId <= 0)
                throw new SocialNetworkException("Chat id is incorrect");

            var chat = Database.ChatRepository.GetByIdWithDetailsAsync(chatId).Result;
            var messages = chat.Messages;

            return _mapper.Map<IEnumerable<MessageModel>>(messages);
        }


        public IEnumerable<ChatModel> GetChatsWithUserByUserId(string userId)
        {
            if (userId == default)
                throw new SocialNetworkException("User id is incorrect");

            var chats = Database.ChatRepository.FindAllWithDetails().Where(x => x.FirstUserId == userId || x.SecondUserId == userId).OrderByDescending(x => x.LastModify).ToList();

            for (int i = 0; i < chats.Count; i++)
            {
                chats[i].Name = chats[i].FirstUserId == userId ? chats[i].SecondUser.UserName : chats[i].FirstUser.UserName;
            }

            return _mapper.Map<IEnumerable<ChatModel>>(chats);
        }

        public async Task<int> GetChatIdByUsernames(string firstUser, string secondUser)
        {
            if (firstUser == default || secondUser == default)
                throw new SocialNetworkException("Usernames are incorrect");

            var chat = Database.ChatRepository.FindAllWithDetails()
                            .Where(x => x.FirstUser.UserName == firstUser && x.SecondUser.UserName == secondUser || x.SecondUser.UserName == firstUser && x.FirstUser.UserName == secondUser)
                            .FirstOrDefault();
            if(chat == default)
            {
                var first = await _userManager.FindByNameAsync(firstUser);
                var second = await _userManager.FindByNameAsync(secondUser);

                await AddAsync(new ChatModel()
                {
                    FirstUserId = first.Id,
                    SecondUserId= second.Id
                });

                chat = Database.ChatRepository.FindAllWithDetails().First(x => x.FirstUserId == first.Id && x.SecondUserId == second.Id);
                return chat.Id;
            }

            return chat.Id;
        }


        public async Task<ChatModel> GetByIdWithDetailsAsync(int id)
        {
            if (id <= 0)
                throw new SocialNetworkException("Not acceptable value id");

            var chat = await Database.ChatRepository.GetByIdWithDetailsAsync(id);
            if (chat == null)
                throw new SocialNetworkException("Chat not found");

            return _mapper.Map<ChatModel>(chat);
        }

        public IEnumerable<ChatModel> GetAll()
        {
            var models = Database.ChatRepository.FindAll().AsEnumerable();
            if(models == null)
                throw new SocialNetworkException("Data not found");

            return _mapper.Map<IEnumerable<ChatModel>>(models);
        }

        public Task AddAsync(ChatModel model)
        {
            if (model == default)
                throw new SocialNetworkException("Null as argument");

            if (model.FirstUserId == default || model.SecondUserId == default)
                throw new SocialNetworkException("Unable to identify chat, users Id or chat Id entered incorrectly");
            model.LastModify = DateTime.Now;

            return Database.ChatRepository.AddAsync(_mapper.Map<Chat>(model));
        }

        public Task UpdateAsync(ChatModel model)
        {
            if (model == default)
                throw new SocialNetworkException("Null as argument");

            if (model.FirstUserId == default || model.SecondUserId == default)
                throw new SocialNetworkException("Unable to identify chat, users Id or chat Id entered incorrectly");

            return Database.ChatRepository.Update(_mapper.Map<Chat>(model));
        }

        public Task DeleteByIdAsync(int modelId)
        {
            if (modelId <= 0)
                throw new SocialNetworkException("Model id is incorrect");

            return Database.UserProfileRepository.DeleteByIdAsync(modelId);
        }
    }
}
