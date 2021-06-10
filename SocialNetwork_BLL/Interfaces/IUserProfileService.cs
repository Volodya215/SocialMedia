using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IUserProfileService : ICrud<UserProfileModel>
    {
        /// <summary>
        /// Finds user profile data by its username 
        /// </summary>
        /// <param name="userName">User username</param>
        /// <returns>User profile</returns>
        UserProfileModel GetUserProfileModelByUserName(string userName);
    }
}
