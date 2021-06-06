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
    public class BloggerSubscriberService : IBloggerSubscriberService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public BloggerSubscriberService(IUnitOfWork iow, IMapper mapper, UserManager<User> userManager)
        {
            Database = iow;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task AddAsync(BloggerSubscriberModel model)
        {
            if (model == default || model.BloggerUserName == default || model.SubscriberUserName == default)
                throw new SocialNetworkException("Incorrect value model");

            if (Database.BloggerSubscriberRepository.IsFriends(model.SubscriberUserName, model.BloggerUserName))
                throw new SocialNetworkException("This element already exist in Database");

            var blogger = await _userManager.FindByNameAsync(model.BloggerUserName);
            var subscriber = await _userManager.FindByNameAsync(model.SubscriberUserName);

            await Database.BloggerSubscriberRepository.AddAsync(new BloggerSubscriber()
            {
                BloggerId = blogger.Id,
                SubscriberId = subscriber.Id
            });
        }

        public async Task DeleteAsync(BloggerSubscriberModel model)
        {
            if (model == default || model.BloggerUserName == default || model.SubscriberUserName == default)
                throw new SocialNetworkException("Incorrect value model");

            var blogSubsc = Database.BloggerSubscriberRepository.FindAllWithDetails()
                                    .Where(x => x.Blogger.UserName == model.BloggerUserName && x.Subscriber.UserName == model.SubscriberUserName)
                                    .FirstOrDefault();
            if (blogSubsc == null)
                throw new SocialNetworkException("Data doesn't exist");
            await Database.BloggerSubscriberRepository.DeleteByIdAsync(blogSubsc.Id);
        }

        public IEnumerable<string> GetAllFollowersByUserName(string userName)
        {
            if (userName == default)
                throw new SocialNetworkException("Incorrect username");

            var followers = Database.BloggerSubscriberRepository.FindAllWithDetails()
                                    .Where(x => x.Blogger.UserName == userName)
                                    .Select(x => x.Subscriber.UserName)
                                    .AsEnumerable();

            return followers;
        }

        public IEnumerable<string> GetAllFollowingByUserName(string userName)
        {
            if (userName == default)
                throw new SocialNetworkException("Incorrect username");

            var following = Database.BloggerSubscriberRepository.FindAllWithDetails()
                                    .Where(x => x.Subscriber.UserName == userName)
                                    .Select(x => x.Blogger.UserName)
                                    .AsEnumerable();

            return following;
        }

        public bool IsFriend(string bloggerUserName, string subscriberUserName)
        {
            if (bloggerUserName == default || subscriberUserName == default)
                throw new SocialNetworkException("Incorrect value model");
            if (subscriberUserName == bloggerUserName)
                return true;

            return Database.BloggerSubscriberRepository.IsFriends(bloggerUserName, subscriberUserName);
        }
    }
}
