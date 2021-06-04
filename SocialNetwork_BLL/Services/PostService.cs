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
    public class PostService : IPostService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        public PostService(IUnitOfWork iow, IMapper mapper)
        {
            Database = iow;
            _mapper = mapper;
        }

        public Task AddAsync(PostModel model)
        {
            if (model == default)
                throw new SocialNetworkException("UserProfile is equal null");

            if (model.Id <= 0 || model.UserId == default || model.Topic == default || model.Content == default)
                throw new SocialNetworkException("Incorrect data in post model");

            return Database.PostRepository.AddAsync(_mapper.Map<Post>(model));
        }

        public Task DeleteByIdAsync(int modelId)
        {
            if (modelId <= 0)
                throw new SocialNetworkException("Model id is incorrect");

            return Database.PostRepository.DeleteByIdAsync(modelId);
        }

        public IEnumerable<PostModel> GetAll()
        {
            var models = Database.PostRepository.FindAll().AsEnumerable();

            return _mapper.Map<IEnumerable<PostModel>>(models);
        }

        public IEnumerable<PostModel> GetAllPostsByUserName(string userName)
        {
            if (userName == default)
                throw new SocialNetworkException("Incorrect value of userName");

            var posts = Database.PostRepository.FindAllWithDetails().Where(x => x.User.UserName == userName).AsEnumerable();

            return _mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<PostModel> GetByIdWithDetailsAsync(int id)
        {
            if (id <= 0)
                throw new SocialNetworkException("Not acceptable value id");
            var post = await Database.PostRepository.GetByIdWithDetailsAsync(id);
            if (post == null)
                throw new SocialNetworkException("Post not found");
            return _mapper.Map<PostModel>(post);
        }

        public Task UpdateAsync(PostModel model)
        {
            if (model == default)
                throw new SocialNetworkException("UserProfile is equal null");

            if (model.Id <= 0 || model.UserId == default || model.Topic == default || model.Content == default)
                throw new SocialNetworkException("Incorrect data in post model");
            return Database.PostRepository.Update(_mapper.Map<Post>(model));
        }
    }
}
