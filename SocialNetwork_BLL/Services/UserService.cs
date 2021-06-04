using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_BLL.Models;
using SocialNetwork_BLL.Validation;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork Database;
        public UserService(IUnitOfWork iow, IMapper mapper, UserManager<User> userManager)
        {
            Database = iow;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<string> LoginUser(LoginModel loginModel, string JWT_secret)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                // Get role assigned to the user
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions identityOptions = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(identityOptions.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_secret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else
                throw new SocialNetworkException("Incorrect login or password in loginModel");
        }

        public async Task<IdentityResult> RegisterUser(UserModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Errors.Any())
                return result;
            await _userManager.AddToRoleAsync(user, model.Role);

            return result;
        }

        public async Task<PageStatistic> GetUserPageStatisticByUserName(string userName)
        {
            if (userName == default)
                throw new SocialNetworkException("Incorrect value in userName");

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new SocialNetworkException("No user with this userName was found");

            var blogSub = Database.BloggerSubscriberRepository.FindAllWithDetails();

            var statistic = new PageStatistic()
            {
                PostsCount = Database.PostRepository.FindAll().Where(x => x.UserId == user.Id).Count(),
                FollowersCount = blogSub.Where(x => x.BloggerId == user.Id).Count(),
                FollowingCount = blogSub.Where(x => x.SubscriberId == user.Id).Count()
            };

            return statistic;
        }
    }
}
