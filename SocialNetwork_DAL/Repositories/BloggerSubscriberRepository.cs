using Microsoft.EntityFrameworkCore;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork_DAL.Repositories
{
    public class BloggerSubscriberRepository : Repository<BloggerSubscriber>, IBloggerSubscriberRepository
    {
        public BloggerSubscriberRepository(SocialNetworkContext myDbContext)
            : base(myDbContext)
        {
        }

        public IQueryable<BloggerSubscriber> FindAllWithDetails()
        {
            return _entities.Include(x => x.Blogger).Include(x => x.Subscriber);
        }

        public bool IsFriends(string bloggerUserName, string subscriberUserName)
        {
            return _entities.Any(x => x.Blogger.UserName == bloggerUserName && x.Subscriber.UserName == subscriberUserName);
        }


        public SocialNetworkContext DbContext
        {
            get { return Context as SocialNetworkContext; }
        }
    }
}
