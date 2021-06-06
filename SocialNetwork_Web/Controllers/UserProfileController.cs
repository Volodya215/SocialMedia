using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_BLL.Models;
using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserProfileService _service;
        public UserProfileController(UserManager<User> userManager, IUserProfileService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        // GET: /api/UserProfile
        public async Task<object> GetUserProfile()
        {
            string userId = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }

        [HttpGet("{userName}")]
        // [Authorize(Roles = "Customer")]
        // GET: /api/UserProfile/Volodya
        public async Task<ActionResult<Object>> GetUserProfile(string userName)
        {
            if (userName == default)
                return BadRequest();
            try
            {
                var userProfile = _service.GetUserProfileModelByUserName(userName);
                var user = await _userManager.FindByNameAsync(userName);

                if (userProfile == default)
                    return Ok(new UserProfileModel());

                return Ok(new
                {
                    user.UserName,
                    user.FullName,
                    userProfile.City,
                    userProfile.Hobby,
                    userProfile.Work,
                    userProfile.About
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


       // [HttpGet("{userName}/followers")]
       //// [Authorize(Roles = "Customer")]
       // // GET: /api/UserProfile/Volodya/followers
       // public ActionResult<IEnumerable<string>> GetUserFollowers(string userName)
       // {
       //     if (userName == default)
       //         return BadRequest();
       //     try
       //     {
       //         var followers = _service.GetListOfFollowersByUserName(userName);
       //         if (followers == default)
       //             return NotFound();

       //         return Ok(followers);
       //     }
       //     catch (Exception)
       //     {
       //         return BadRequest();
       //     }
       // }

       // [HttpGet("{userName}/following")]
       //// [Authorize(Roles = "Customer")]
       // // GET: /api/UserProfile/Volodya/following
       // public ActionResult<IEnumerable<string>> GetUserFollowing(string userName)
       // {
       //     if (userName == default)
       //         return BadRequest();
       //     try
       //     {
       //         var followers = _service.GetListOfFollowingByUserName(userName);
       //         if (followers == default)
       //             return NotFound();

       //         return Ok(followers);
       //     }
       //     catch (Exception)
       //     {
       //         return BadRequest();
       //     }
       // }

    }
}
