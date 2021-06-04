using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IBloggerSubscriberRepository : IRepository<BloggerSubscriber>
    {
        IQueryable<BloggerSubscriber> FindAllWithDetails();
    }
}
