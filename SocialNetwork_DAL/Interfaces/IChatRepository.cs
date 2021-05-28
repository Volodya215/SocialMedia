using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        IQueryable<Chat> FindAllWithDetails();

        Task<Chat> GetByIdWithDetailsAsync(int id);
    }
}
