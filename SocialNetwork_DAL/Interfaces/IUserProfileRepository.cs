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
        /// <summary>
        /// Finds all user profiles with detailed information about them 
        /// </summary>
        /// <returns>Returns a query string with an expression tree </returns>
        IQueryable<UserProfile> FindAllWithDetails();

        /// <summary>
        /// Finds user profile with detailed information about them by id
        /// </summary>
        /// <param name="id">Id of user ptofile in database</param>
        /// <returns>A task that represents the asynchronous user profile data</returns>
        Task<UserProfile> GetByIdWithDetailsAsync(int id);
    }
}
