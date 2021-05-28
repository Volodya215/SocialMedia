using Microsoft.EntityFrameworkCore;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(SocialNetworkContext myDbContext)
            : base(myDbContext)
        {
        }

        public IQueryable<UserProfile> FindAllWithDetails()
        {
            return _entities.Include(x => x.User).Include(x => x.Followers).Include(x => x.Following).Include(x => x.Posts);
        }

        public Task<UserProfile> GetByIdWithDetailsAsync(int id)
        {
            return _entities.Include(x => x.User).Include(x => x.Followers).Include(x => x.Following).Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id == id);
        }

        public SocialNetworkContext DbContext
        {
            get { return Context as SocialNetworkContext; }
        }
    }
}
