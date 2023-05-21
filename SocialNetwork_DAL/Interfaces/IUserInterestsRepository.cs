using SocialNetwork_DAL.Entities;
using System.Linq;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IUserInterestsRepository : IRepository<UserInterests>
    {
        IQueryable<UserInterests> FindAllUserInterestsById(string userId);
    }
}
