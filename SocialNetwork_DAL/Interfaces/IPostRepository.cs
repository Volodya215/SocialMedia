using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IQueryable<Post> FindAllWithDetails();

        Task<Post> GetByIdWithDetailsAsync(int id);
    }
}
