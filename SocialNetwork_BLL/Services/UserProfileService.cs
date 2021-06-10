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
    /// <summary>
    /// Service for processing data related to user profiles
    /// </summary>
    public class UserProfileService : IUserProfileService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork Database;
        /// <summary>
        /// Injection dependence in this service 
        /// </summary>
        /// <param name="iow">Class Unit of Work</param>
        /// <param name="mapper">Mapper for mapping data</param>
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


            return _mapper.Map<UserProfileModel>(userProfile);
        }

        public Task UpdateAsync(UserProfileModel model)
        {
            if (model == default)
                throw new SocialNetworkException("UserProfile is equal null");

            var userProfile = Database.UserProfileRepository.FindAllWithDetails().FirstOrDefault(x => x.UserId == model.UserId);
            if (userProfile == default)
                return AddAsync(model);
            else
            {
                userProfile.Hobby = model.Hobby;
                userProfile.City = model.City;
                userProfile.Work = model.Work;
                userProfile.About = model.About;
            }

            return Database.UserProfileRepository.Update(userProfile);

        }

        public Task AddAsync(UserProfileModel model)
        {
            if (model == default || model.UserId == default)
                throw new SocialNetworkException("UserProfile is equal null or userId entered incorrectly");

            var isProfileExist = Database.UserProfileRepository.FindAllWithDetails().Any(x => x.UserId == model.UserId);
            if (isProfileExist )
                throw new SocialNetworkException("This userProfile already exist in database");

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
