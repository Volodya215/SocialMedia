using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_BLL.Models;
using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationSettings _appSettings;
        private readonly IUserService _service;
        private readonly UserManager<User> _userManager;

        public UserController(IOptions<ApplicationSettings> appSettings, IUserService service, UserManager<User> userManager)
        {
            _appSettings = appSettings.Value;
            _service = service;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> PostUser(UserModel model)
        {
            if (model == default)
                return BadRequest("Empty model");

            try
            {
                var result = await _service.RegisterUser(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/User/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model == default)
                return BadRequest("Empty model");
            try
            {
                var token = await _service.LoginUser(model, _appSettings.JWT_Secret);
                return Ok(new { token, model.UserName });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("{userName}/statistic")]
        // [Authorize(Roles = "Customer")]
        // GET: /api/User/Volodya/statistic
        public async Task<ActionResult<Object>> GetUserStatistic(string userName)
        {
            if (userName == default)
                return BadRequest();
            try
            {
                var pageStatistic = await _service.GetUserPageStatisticByUserName(userName);
                if (pageStatistic == default)
                    return Ok(new PageStatistic());

                return Ok(pageStatistic);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("allUsers")]
        [Authorize(Roles = "Admin, Customer")]
        // GET: /api/User/allUsers
        public async Task<ActionResult<IEnumerable<string>>> GetAllUsers()
        {
            try
            {
                var users = await _service.GetAllUser();

                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Customer")]
        // PUT: /api/User/Update
        public async Task<ActionResult> PutUser(UserModel userModel)
        {
            try
            {
                string userId = User.Claims.First(x => x.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                user.Email = userModel.Email;
                user.FullName = userModel.FullName;

                var result = await _userManager.UpdateAsync(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
