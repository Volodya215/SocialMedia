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
    /// Repository for working with chats data 
    /// </summary>
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        /// <summary>
        /// Repository constructor in which transfer a context for work with a database
        /// </summary>
        /// <param name="myDbContext">Context for work with SocialNetwork database</param>
        public ChatRepository(SocialNetworkContext myDbContext)
            : base(myDbContext)
        {
        }

        public IQueryable<Chat> FindAllWithDetails()
        {
            return _entities.Include(x => x.FirstUser).Include(x => x.SecondUser).Include(x => x.Messages);
        }

        public Task<Chat> GetByIdWithDetailsAsync(int id)
        {
            return _entities.Include(x => x.FirstUser).Include(x => x.SecondUser).Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id == id);
        }

        public SocialNetworkContext DbContext
        {
            get { return Context as SocialNetworkContext; }
        }
    }
}
