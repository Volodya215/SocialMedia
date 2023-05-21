using SocialNetwork_BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IInterestsService
    {
        IEnumerable<InterestModel> GetFullListOfInterests();
        Task<IEnumerable<InterestModel>> GetListOfUserInterests(string userName);
        Task AddUserInterestAsync(string userName, int interestId);
        Task DeleteUserInterest(string userName, int interestId);
    }
}
