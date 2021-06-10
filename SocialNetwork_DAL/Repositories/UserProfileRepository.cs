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
    /// <summary>
    /// Repository for working with user profile data 
    /// </summary>
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        /// <summary>
        /// Repository constructor in which transfer a context for work with a database
        /// </summary>
        /// <param name="myDbContext">Context for work with SocialNetwork database</param>
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
