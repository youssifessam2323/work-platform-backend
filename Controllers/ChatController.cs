using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly TeamChatService teamChatService;
        private readonly UserService userService;

        public ChatController(TeamChatService teamChatService,UserService userService)
        {
            this.teamChatService = teamChatService;
            this.userService = userService;
        }


       
        [HttpDelete]
        [Route("{chatId}")]
        public async Task<IActionResult> DeletTeamChat(int chatId)
        {
            try
            {
                await teamChatService.DeleteTeamChat(chatId);
                return Ok();
            }
            catch (DbUpdateException ex)
            {

                return BadRequest($"chat with {chatId} Not Found");
            }

        }
    }
}
