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
        public ChatController(IChatService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get chat id by usernames of his members
        /// </summary>
        /// <param name="firstUsername"></param>
        /// <param name="secondUsername"></param>
        /// <returns>Chat id</returns>
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

        /// <summary>
        /// Get all user chats by his id
        /// </summary>
        /// <returns>List of chats</returns>
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

        /// <summary>
        /// Get all messages from chat by chat id
        /// </summary>
        /// <param name="chatId">Chat id</param>
        /// <returns>List of messages</returns>
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

        /// <summary>
        /// Delete chat by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
