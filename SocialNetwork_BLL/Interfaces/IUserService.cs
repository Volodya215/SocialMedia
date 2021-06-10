using Microsoft.AspNetCore.Identity;
using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Registers the user in the system 
        /// </summary>
        /// <param name="model">User data </param>
        /// <returns>Registration result </returns>
        Task<IdentityResult> RegisterUser(UserModel model);

        /// <summary>
        /// Login user in the system 
        /// </summary>
        /// <param name="loginModel">Model with username and password</param>
        /// <param name="JWT_secret">The key to create a token </param>
        /// <returns>Authorization token </returns>
        Task<string> LoginUser(LoginModel loginModel, string JWT_secret);

        /// <summary>
        /// Defines statistics per user page as the number of posts, subscribers, and those to whom it is signed  
        /// </summary>
        /// <param name="userName">User username</param>
        /// <returns>Page statistic</returns>
        Task<PageStatistic> GetUserPageStatisticByUserName(string userName);

        /// <summary>
        /// Identifies all users in the system with role 'Customer'
        /// </summary>
        /// <returns>List of all usernames</returns>
        Task<IEnumerable<string>> GetAllUsernames();

        /// <summary>
        /// Identifies all users in the system with role 'Customer'
        /// </summary>
        /// <returns>List of all users</returns>
        Task<IEnumerable<UserModel>> GetAllUsers();

        /// <summary>
        /// Asynchronously updates user data in the database
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="model">User model</param>
        /// <returns>Result of updating</returns>
        Task<IdentityResult> UpdateUser(string id, UserModel model);

        /// <summary>
        /// Removes the user from the database 
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Result of deleting</returns>
        Task<IdentityResult> DeleteUser(string id);
    }
}
