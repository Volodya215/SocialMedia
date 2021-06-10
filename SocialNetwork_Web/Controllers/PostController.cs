using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        public PostController(IPostService service)
        {
            _service = service;
        }

        [HttpGet("{userName}")]
        [Authorize(Roles = "Customer")]
        // GET: /api/Post/Volodya
        public ActionResult<Object> GetAllUserPostByUserName(string userName)
        {
            if (userName == default)
                return BadRequest();
            try
            {
                var posts = _service.GetAllPostsByUserName(userName);

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        // POST: /api/Post
        public async Task<ActionResult> PostAdding(PostModel postModel)
        {
            if (postModel == default || postModel.Topic == default || postModel.Content == default)
                return BadRequest("Incorrect data in post");

            try
            {
                string userId = User.Claims.First(x => x.Type == "UserID").Value;
                postModel.DateOfPost = DateTime.Now;
                postModel.UserId = userId;

                await _service.AddAsync(postModel);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
