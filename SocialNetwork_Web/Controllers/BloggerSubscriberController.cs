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
    public class BloggerSubscriberController : ControllerBase
    {
        private readonly IBloggerSubscriberService _service;
        public BloggerSubscriberController(IBloggerSubscriberService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds a new subscriber 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Customer")]
        // POST: /api/BloggerSubscriber
        public async Task<ActionResult> PostBlogSubsc(BloggerSubscriberModel model)
        {
            if (model == default || model.BloggerUserName == default || model.SubscriberUserName == default)
                return BadRequest();

            try
            {
                await _service.AddAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get all user followers
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>List of usernames of followers</returns>
        [HttpGet("{userName}/followers")]
        [Authorize(Roles = "Customer")]
        // GET: /api/BloggerSubscriber/Volodya/followers
        public ActionResult<IEnumerable<string>> GetUserFollowers(string userName)
        {
            if (userName == default)
                return BadRequest();
            try
            {
                var followers = _service.GetAllFollowersByUserName(userName);
                if (followers == default)
                    return NotFound();

                return Ok(followers);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get all user following
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>List of usernames of following</returns>
        [HttpGet("{userName}/following")]
        [Authorize(Roles = "Customer")]
        // GET: /api/BloggerSubscriber/Volodya/following
        public ActionResult<IEnumerable<string>> GetUserFollowing(string userName)
        {
            if (userName == default)
                return BadRequest();
            try
            {
                var followers = _service.GetAllFollowingByUserName(userName);
                if (followers == default)
                    return NotFound();

                return Ok(followers);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Checks if the user is subscribed to this blogger 
        /// </summary>
        /// <param name="bloggerUserName"></param>
        /// <param name="subscriberUserName"></param>
        /// <returns>True if subscribe, otherwise false</returns>
        [HttpGet("isFriends/{bloggerUserName}/{subscriberUserName}")]
        [Authorize(Roles = "Customer")]
        // GET: /api/BloggerSubscriber/isFriend/bloggerUserName/subscriberUserName
        public ActionResult<bool> IsFriends(string bloggerUserName, string subscriberUserName)
        {
            if (bloggerUserName == default || subscriberUserName == default)
                return BadRequest();
            try
            {
                var isFriend = _service.IsFriend(bloggerUserName, subscriberUserName);

                return Ok(isFriend);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete user subscribe
        /// </summary>
        /// <param name="bloggerUserName"></param>
        /// <param name="subscriberUserName"></param>
        /// <returns></returns>
        [HttpDelete("{bloggerUserName}/{subscriberUserName}")]
        [Authorize(Roles = "Customer")]
        // DELETE: /api/BloggerSubscriber
        public async Task<ActionResult> DeleteBlogSubsc(string bloggerUserName, string subscriberUserName)
        {
            if (bloggerUserName == default || subscriberUserName == default)
                return BadRequest();

            try
            {
                await _service.DeleteAsync(bloggerUserName, subscriberUserName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
