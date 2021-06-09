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
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;
        private readonly IMessageService _messageService;
        public ChatController(IChatService service, IMessageService messageService)
        {
            _service = service;
            _messageService = messageService;
        }

        [HttpGet("GetChatId/{firstUsername}/{secondUsername}")]
        [Authorize(Roles = "Customer")]
        // GET: /api/Chat/GetChatId/vasya/Volodya
        public async Task<ActionResult<int>> GetChatId(string firstUsername, string secondUsername)
        {
            if (firstUsername == default || secondUsername == default)
                return BadRequest("Incorrect usernames");

            try
            {
                var chatId = await _service.GetChatIdByUsernames(firstUsername, secondUsername);
                return Ok(chatId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("AllUserChats")]
        [Authorize(Roles = "Customer")]
        // GET: /api/Chat/AllUserChats
        public ActionResult<int> GetAllUserChats()
        {
            try
            {
                string userId = User.Claims.First(x => x.Type == "UserID").Value;
                var chats = _service.GetChatsWithUserByUserId(userId);
                return Ok(chats);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{chatId}/AllMessages")]
        [Authorize(Roles = "Customer")]
        // GET: /api/Chat/1/AllMessages
        public ActionResult<Object> GetAllMessagesFromChat(int chatId)
        {
            if (chatId <= 0)
                return BadRequest();
            try
            {
                string userId = User.Claims.First(x => x.Type == "UserID").Value;
                var messages = _service.GetAllMessagesFromChatByChatId(chatId);
                return Ok(new { messages = messages, id = userId});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("AddMessage")]
        [Authorize(Roles = "Customer")]
        // POST: /api/Chat/AddMessage
        public async Task<ActionResult> AddMessageToChat(MessageModel messageModel)
        {
            if (messageModel == default || messageModel.ChatId < 0 || messageModel.Content == default)
                return BadRequest("Incorrect model");
            try
            {
                string userId = User.Claims.First(x => x.Type == "UserID").Value;
                messageModel.AuthorId = userId;
                await _messageService.AddAsync(messageModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            if (id <= 0)
                return BadRequest("Incorrect id");

            try
            {
               await  _service.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
