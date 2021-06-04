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

        public UserController(IOptions<ApplicationSettings> appSettings, IUserService service)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }

        //[HttpPost]
        //[Route("Register")]
        ////POST : /api/ApplicationUser/Register
        //public async Task<Object> PostUser(UserModel model)
        //{
        //    var user = new User()
        //    {
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        FullName = model.FullName
        //    };

        //    try
        //    {
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        await _userManager.AddToRoleAsync(user, model.Role);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

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
                return Ok(new { token });
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

        //[HttpPost]
        //[Route("Login")]
        ////POST : /api/ApplicationUser/Login
        //public async Task<IActionResult> Login(LoginModel model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.UserName);

        //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        // Get role assigned to the user
        //        var role = await _userManager.GetRolesAsync(user);
        //        IdentityOptions identityOptions = new IdentityOptions();

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim("UserID", user.Id.ToString()),
        //                new Claim(identityOptions.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
        //            }),
        //            Expires = DateTime.UtcNow.AddDays(1),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
        //        };

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        //        var token = tokenHandler.WriteToken(securityToken);
        //        return Ok(new { token });
        //    }
        //    else
        //        return BadRequest(new { message = "Username or password is incorrect." });
        //}
    }
}
