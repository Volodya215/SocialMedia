using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_BLL.Models;
using System;
using System.Threading.Tasks;

namespace SocialNetwork_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestsService _interestService;
        public InterestsController(IInterestsService interestService)
        {
            _interestService = interestService;
        }

        [HttpGet("{userName}")]
        [Authorize(Roles = "Customer")]
        // GET: /api/Interests/Volodya
        public async Task<ActionResult<InterestModel>> GetAllUserInterestByUserName(string userName)
        {
            if (userName == default)
                return BadRequest();

            try
            {
                var interests = await _interestService.GetListOfUserInterests(userName);
                return Ok(interests);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("All")]
        [Authorize(Roles = "Customer")]
        // GET: /api/Interests/All
        public ActionResult<InterestModel> GetAllInterest()
        {
            try
            {
                var interests = _interestService.GetFullListOfInterests();
                return Ok(interests);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("AddUserInterest")]
        //[Authorize(Roles = "Customer")]
        // POST: /api/Interests/AddUserInterest
        public async Task<ActionResult> PostAddUserInterest(UserInterestModel userInterest)
        {
            if (string.IsNullOrEmpty(userInterest.UserName))
                return BadRequest("Incorrect userName.");

            if (userInterest.InterestId <= 0)
                return BadRequest($"Incorrect interest Id: {userInterest.InterestId}");

            try
            {
                await _interestService.AddUserInterestAsync(userInterest.UserName, userInterest.InterestId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{userName}/{interestId}")]
        [Authorize(Roles = "Customer")]
        // DELETE: /api/Interests
        public async Task<ActionResult> DeleteUserInterest(string userName, int interestId)
        {
            if (string.IsNullOrEmpty(userName) || interestId <= 0)
                return BadRequest();

            try
            {
                await _interestService.DeleteUserInterest(userName, interestId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
