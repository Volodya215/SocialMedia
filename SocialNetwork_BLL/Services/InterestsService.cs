using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_BLL.Models;
using SocialNetwork_BLL.Validation;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Services
{
    public class InterestsService : IInterestsService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Injection dependence in this service 
        /// </summary>
        /// <param name="iow">Unit of Work</param>
        /// <param name="mapper">Mapper for mapping data</param>
        /// <param name="userManager">To work with the date of the user in the database </param>
        public InterestsService(IUnitOfWork iow, IMapper mapper, UserManager<User> userManager)
        {
            Database = iow;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task AddUserInterestAsync(string userName, int interestId)
        {
            if (string.IsNullOrEmpty(userName))
                throw new SocialNetworkException("Incorrect userName.");

            if (interestId <= 0)
                throw new SocialNetworkException($"Incorrect interestId: {interestId}.");

            var blogger = await _userManager.FindByNameAsync(userName);
            if (blogger is null)
                throw new SocialNetworkException($"Cannot find blogger with name: {userName}");
            var interest = await Database.InterestRepository.GetByIdAsync(interestId);
            if (interest is null)
                throw new SocialNetworkException($"Interest with id: {interestId} not found.");

            var userInterest = Database.UserInterestsRepository.FindAllUserInterestsById(blogger.Id)
                .FirstOrDefault(x => x.InterestId == interestId);

            if(userInterest is null)
            {
                await Database.UserInterestsRepository.AddAsync(new UserInterests()
                {
                    UserId = blogger.Id,
                    InterestId = interest.Id
                });
            }
        }

        public async Task DeleteUserInterest(string userName, int interestId)
        {
            if (string.IsNullOrEmpty(userName))
                throw new SocialNetworkException("Incorrect userName.");

            if (interestId <= 0)
                throw new SocialNetworkException($"Incorrect interestId: {interestId}.");

            var blogger = await _userManager.FindByNameAsync(userName);
            if (blogger is null)
                throw new SocialNetworkException($"Cannot find blogger with name: {userName}");
            var userInterest = Database.UserInterestsRepository.FindAllUserInterestsById(blogger.Id)
                .FirstOrDefault(x => x.InterestId == interestId);

            if(userInterest != null)
                await Database.UserInterestsRepository.DeleteByIdAsync(userInterest.Id);
        }

        public IEnumerable<InterestModel> GetFullListOfInterests()
        {
            var interests = Database.InterestRepository.FindAll().ToList();
            return _mapper.Map<IEnumerable<InterestModel>>(interests);
        }

        public async Task<IEnumerable<InterestModel>> GetListOfUserInterests(string userName)
        {
            var blogger = await _userManager.FindByNameAsync(userName);
            if (blogger is null)
                throw new SocialNetworkException($"Cannot find blogger with name: {userName}");

            var interests = Database.UserInterestsRepository.FindAllUserInterestsById(blogger.Id);
            return _mapper.Map<IEnumerable<InterestModel>>(interests);
        }
    }
}
