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

        [HttpPost]
        // [Authorize(Roles = "Customer")]
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

        [HttpGet("followers")]
        // [Authorize(Roles = "Customer")]
        // GET: /api/BloggerSubscriber/followers
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

        [HttpGet("following")]
        // [Authorize(Roles = "Customer")]
        // GET: /api/BloggerSubscriber/following
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

        [HttpDelete]
        // [Authorize(Roles = "Customer")]
        // DELETE: /api/BloggerSubscriber
        public async Task<ActionResult> DeleteBlogSubsc(BloggerSubscriberModel model)
        {
            if (model == default || model.BloggerUserName == default || model.SubscriberUserName == default)
                return BadRequest();

            try
            {
                await _service.DeleteAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
