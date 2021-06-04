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
        Task<IdentityResult> RegisterUser(UserModel model);

        Task<string> LoginUser(LoginModel loginModel, string JWT_secret);

        Task<PageStatistic> GetUserPageStatisticByUserName(string userName);
    }
}
