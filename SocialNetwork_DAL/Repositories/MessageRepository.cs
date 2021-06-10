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
    /// Repository for working with messages data 
    /// </summary>
    class MessageRepository : Repository<Message>, IMessageRepository
    {
        /// <summary>
        /// Repository constructor in which transfer a context for work with a database
        /// </summary>
        /// <param name="myDbContext">Context for work with SocialNetwork database</param>
        public MessageRepository(SocialNetworkContext myDbContext)
            : base(myDbContext)
        {
        }

        public IQueryable<Message> FindAllWithDetails()
        {
            return _entities.Include(x => x.Author).Include(x => x.Chat);
        }

        public Task<Message> GetByIdWithDetailsAsync(int id)
        {
            return _entities.Include(x => x.Author).Include(x => x.Chat).FirstOrDefaultAsync(x => x.Id == id);
        }

        public SocialNetworkContext DbContext
        {
            get { return Context as SocialNetworkContext; }
        }
    }
}
