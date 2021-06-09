using AutoMapper;
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
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        public MessageService(IUnitOfWork iow, IMapper mapper)
        {
            Database = iow;
            _mapper = mapper;
        }

        public async Task AddAsync(MessageModel model)
        {
            if (model == default)
                throw new SocialNetworkException("Message is equal null");
            if (model.ChatId <= 0 || model.AuthorId == default || model.Content == default)
                throw new SocialNetworkException("Incorrect data in message model");

            var chat = await Database.ChatRepository.GetByIdAsync(model.ChatId);
            chat.LastModify = DateTime.Now;
            await Database.ChatRepository.Update(chat);

            model.MessageTime = DateTime.Now;

           await  Database.MessageRepository.AddAsync(_mapper.Map<Message>(model));
        }

        public Task DeleteByIdAsync(int modelId)
        {
            if (modelId <= 0)
                throw new SocialNetworkException("Model id is incorrect");

            return Database.MessageRepository.DeleteByIdAsync(modelId);
        }

        public IEnumerable<MessageModel> GetAll()
        {
            var models = Database.MessageRepository.FindAll().AsEnumerable();

            return _mapper.Map<IEnumerable<MessageModel>>(models);
        }

        public IEnumerable<MessageModel> GetAllUserMessagesByUserId(string id)
        {
            if (id == default)
                throw new SocialNetworkException("Not acceptable value id");

            var messages = Database.MessageRepository.FindAll().Where(x => x.AuthorId == id).AsEnumerable();

            return _mapper.Map<IEnumerable<MessageModel>>(messages);
        }

        public async Task<MessageModel> GetByIdWithDetailsAsync(int id)
        {
            if (id <= 0)
                throw new SocialNetworkException("Not acceptable value id");

            var message = await Database.MessageRepository.GetByIdWithDetailsAsync(id);

            return _mapper.Map<MessageModel>(message);
        }

        public Task UpdateAsync(MessageModel model)
        {
            if (model == default)
                throw new SocialNetworkException("Message is equal null");
            if (model.Id <= 0 || model.ChatId <= 0 || model.AuthorId == default || model.Content == default)
                throw new SocialNetworkException("Incorrect data in message model");

            return Database.MessageRepository.Update(_mapper.Map<Message>(model));
        }
    }
}
