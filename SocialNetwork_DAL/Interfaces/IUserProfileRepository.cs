using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        IQueryable<UserProfile> FindAllWithDetails();

        Task<UserProfile> GetByIdWithDetailsAsync(int id);
    }
}
