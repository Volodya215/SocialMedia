using Microsoft.EntityFrameworkCore;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System.Linq;

namespace SocialNetwork_DAL.Repositories
{
    public class UserInterestsRepository : Repository<UserInterests>, IUserInterestsRepository
    {
        public UserInterestsRepository(SocialNetworkContext myDbContext) : base(myDbContext)
        {}

        public IQueryable<UserInterests> FindAllUserInterestsById(string userId)
        {
            return _entities.Include(x => x.User).Include(x => x.Interest).Where(x => x.UserId == userId);
        }
    }
}
