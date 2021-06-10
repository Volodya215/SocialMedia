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
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;
        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        // GET: /api/Message/GetAll
        public ActionResult<IEnumerable<MessageAdminModel>> GetAllForAdmin()
        {
            try
            {
                var messages = _service.GetAllMessagesForAdmin();

                return Ok(messages);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("AddMessage")]
        [Authorize(Roles = "Customer")]
        // POST: /api/Message/AddMessage
        public async Task<ActionResult> AddMessageToChat(MessageModel messageModel)
        {
            if (messageModel == default || messageModel.ChatId < 0 || messageModel.Content == default)
                return BadRequest("Incorrect model");
            try
            {
                string userId = User.Claims.First(x => x.Type == "UserID").Value;
                messageModel.AuthorId = userId;
                await _service.AddAsync(messageModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        // DELETE: /api/Message/Update
        public async Task<IActionResult> UpdateMessage(MessageModel model)
        {
            if (model == default)
                return BadRequest();

            try
            {
                await _service.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        // DELETE: /api/Message/Delete/1
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                await _service.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
