using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IUserProfileService : ICrud<UserProfileModel>
    {
        UserProfileModel GetUserProfileModelByUserName(string userName);
    }
}
