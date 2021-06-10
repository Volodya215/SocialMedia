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
    /// Repository for working with posts data 
    /// </summary>
    public class PostRepository : Repository<Post>, IPostRepository
    {
        /// <summary>
        /// Repository constructor in which transfer a context for work with a database
        /// </summary>
        /// <param name="myDbContext">Context for work with SocialNetwork database</param>
        public PostRepository(SocialNetworkContext myDbContext)
            : base(myDbContext)
        {
        }


        public IQueryable<Post> FindAllWithDetails()
        {
            return _entities.Include(x => x.User);
        }

        public Task<Post> GetByIdWithDetailsAsync(int id)
        {
            return _entities.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public SocialNetworkContext DbContext
        {
            get { return Context as SocialNetworkContext; }
        }
    }
}
