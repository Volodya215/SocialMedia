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
            return _entities.Include(x => x.User);
        }

        public Task<UserProfile> GetByIdWithDetailsAsync(int id)
        {
            return _entities.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public SocialNetworkContext DbContext
        {
            get { return Context as SocialNetworkContext; }
        }
    }
}
