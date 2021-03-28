using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [Route("api/v1/chatMessage")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {

        private readonly UserService userService;
        private readonly ChatMessageService chatMessageService;

        public ChatMessageController(ChatMessageService chatMessageService, UserService userService)
        {
            this.chatMessageService = chatMessageService;
            this.userService = userService;

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("{toTeamChatId}")]
        public async Task<IActionResult> AddMessage(ChatMessage chatMessage, int toTeamChatId)
        {
           
            
                var newChatTeam = await chatMessageService.CreateMessage(chatMessage, userService.GetUserId(), toTeamChatId);
                if (newChatTeam != null)
                {
                    return Ok(newChatTeam);
                }
        
                return BadRequest("Message error not sent !!");
            

        }


        [HttpDelete]
        [Route("{messageChatId}")]
        public async Task<IActionResult> DeletMessage(int messageChatId)
        {
            try
            {
                await chatMessageService.DeleteMessage(messageChatId);
            }
            catch (DbUpdateException ex)
            {

                return BadRequest("Message Not Found To Delete ");
            }

            return Ok($"Message with id = {messageChatId} was  Deleted");
        }


        [HttpGet]
        [Route("{messageId}")]
        public async Task<IActionResult> GetMessageById(int messageId)
        {

            var Message = await chatMessageService.GetMessage(messageId);
            return Ok(Message);

        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("GetMessageOfUser")]
        public async Task<IActionResult> GetMessagesOfUser()
        {

            var AllMessages = await chatMessageService.GetAllMessagesOfUser(userService.GetUserId());
            if (AllMessages == null)
            {
                return Ok(new List<ChatMessage>());

            }
            return Ok(AllMessages);
        }

        [HttpGet]
        [Route("{teamChat}/TeamChat")]
        public async Task<IActionResult> GetMessagesOfTeamChat(int teamChat)
        {

            var AllMessages = await chatMessageService.GetAllMessagesOfTeamChat(teamChat);
            if (AllMessages == null)
            {
                return Ok(new List<ChatMessage>());

            }
            return Ok(AllMessages);
        }

        [HttpGet]
        [Route("{messageType}/MessageType")]
        public async Task<IActionResult> GetMessagesOfMessageType(int messageType)
        {

            var AllMessages = await chatMessageService.GetAllMessagesOfMessageType(messageType);
            if (AllMessages == null)
            {
                return Ok(new List<ChatMessage>());

            }
            return Ok(AllMessages);
        }




    }
}
