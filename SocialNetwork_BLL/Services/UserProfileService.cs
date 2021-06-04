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
    public class UserProfileService : IUserProfileService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork Database;
        public UserProfileService(IUnitOfWork iow, IMapper mapper)
        {
            Database = iow;
            _mapper = mapper;
        }


        public UserProfileModel GetUserProfileModelByUserName(string userName)
        {
            if (userName == default)
                throw new SocialNetworkException("Incorect username");

            var userProfile = Database.UserProfileRepository.FindAllWithDetails().Where(x => x.User.UserName == userName).FirstOrDefault();

            if (userProfile == default)
                throw new SocialNetworkException("User with that userName not found!!!");

            return _mapper.Map<UserProfileModel>(userProfile);
        }

        public Task UpdateAsync(UserProfileModel model)
        {
            if (model == default)
                throw new SocialNetworkException("UserProfile is equal null");
            bool isNum = int.TryParse(model.UserId, out int num);

            if (model.Id <= 0 || !isNum || num <= 0)
                throw new SocialNetworkException("Unable to identify model, id entered incorrectly ");

            var userProfile = _mapper.Map<UserProfile>(model);
            return Database.UserProfileRepository.Update(userProfile);

        }

        public Task AddAsync(UserProfileModel model)
        {
            if (model == default)
                throw new SocialNetworkException("UserProfile is equal null");

            if (model.UserId == default)
                throw new SocialNetworkException("Unable to identify user, userId entered incorrectly ");

            return Database.UserProfileRepository.AddAsync(_mapper.Map<UserProfile>(model));
        }

        public IEnumerable<UserProfileModel> GetAll()
        {
            var models = Database.UserProfileRepository.FindAll().AsEnumerable();
            if (models == null)
                throw new SocialNetworkException("Data not found");

            return _mapper.Map<IEnumerable<UserProfileModel>>(models);
        }

        public async Task<UserProfileModel> GetByIdWithDetailsAsync(int id)
        {
            if (id <= 0)
                throw new SocialNetworkException("Not acceptable value id");

            var userProfile = await Database.UserProfileRepository.GetByIdWithDetailsAsync(id);
            if (userProfile == null)
                throw new SocialNetworkException("Data not found");

            return _mapper.Map<UserProfileModel>(userProfile);
        }

        public Task DeleteByIdAsync(int modelId)
        {
            if (modelId <= 0)
                throw new SocialNetworkException("Model id is incorrect");

            return Database.UserProfileRepository.DeleteByIdAsync(modelId);
        }
    }
}
