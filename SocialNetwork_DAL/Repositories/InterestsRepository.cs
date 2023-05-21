using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;

namespace SocialNetwork_DAL.Repositories
{
    public class InterestsRepository : Repository<Interest>, IInterestRepository
    {
        public InterestsRepository(SocialNetworkContext myDbContext) : base(myDbContext)
        { }
    }
}
