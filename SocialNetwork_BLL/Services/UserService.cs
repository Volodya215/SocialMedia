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
    /// <summary>
    /// Service for processing data related to users
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork Database;
        /// <summary>
        /// Injection dependence in this service 
        /// </summary>
        /// <param name="iow">Class Unit of Work</param>
        /// <param name="mapper">Mapper for mapping data</param>
        /// <param name="userManager">To work with the date of the user in the database </param>
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
                    Expires = DateTime.UtcNow.AddDays(1),
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

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Errors.Any())
                    return result;
                await _userManager.AddToRoleAsync(user, model.Role);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

        public async Task<IEnumerable<string>> GetAllUsernames()
        {
            var users = await _userManager.GetUsersInRoleAsync("Customer");

            return users.Select(x => x.UserName);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Customer");

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<IdentityResult> UpdateUser(string id, UserModel model)
        {
            if(id == default)
                throw new SocialNetworkException("Incorrect id value");

            var user = await _userManager.FindByIdAsync(id);

            user.Email = model.Email;
            user.FullName = model.FullName;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            if (id == default)
                throw new SocialNetworkException("Incorrect id value");

            var user = await _userManager.FindByIdAsync(id);
            var subs = Method(id);

            foreach (var item in subs)
            {
                await Database.BloggerSubscriberRepository.DeleteByIdAsync(item);
            }


            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public List<int> Method(string id)
        {
            var subs = Database.BloggerSubscriberRepository.FindAll().Where(x => x.BloggerId == id || x.SubscriberId == id).Select(x => x.Id).ToList();
            return subs;
        }
    }
}
