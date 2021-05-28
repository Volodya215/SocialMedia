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
    class MessageRepository : Repository<Message>, IMessageRepository
    {
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
