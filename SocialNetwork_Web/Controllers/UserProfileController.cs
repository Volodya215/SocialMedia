﻿using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Get user profile by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User profile</returns>
        [HttpGet("{userName}")]
        [Authorize(Roles = "Customer")]
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
                    userProfile = new UserProfileModel();
                

                return Ok(new
                {
                    user.UserName,
                    user.Email,
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

        /// <summary>
        /// Update exist user profile
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [Authorize(Roles = "Customer")]
        // PUT: /api/UserProfile/Update
        public async Task<ActionResult> PutUser(UserProfileModel userModel)
        {
            try
            {
                string userId = User.Claims.First(x => x.Type == "UserID").Value;
                userModel.UserId = userId;

                await _service.UpdateAsync(userModel);

                return Ok("Changed");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
